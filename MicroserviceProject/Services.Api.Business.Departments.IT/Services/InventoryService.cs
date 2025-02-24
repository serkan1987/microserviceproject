﻿using AutoMapper;

using Infrastructure.Caching.Abstraction;
using Infrastructure.Communication.Http.Wrapper;
using Infrastructure.Localization.Translation.Provider;
using Infrastructure.Transaction.Recovery;
using Infrastructure.Transaction.UnitOfWork.Sql;

using Services.Api.Business.Departments.IT.Entities.Sql;
using Services.Api.Business.Departments.IT.Repositories.Sql;
using Services.Communication.Http.Broker.Department.IT.Models;
using Services.Communication.Mq.Queue.Buying.Models;
using Services.Communication.Mq.Queue.Buying.Rabbit.Publishers;
using Services.Logging.Aspect.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Api.Business.Departments.IT.Services
{
    /// <summary>
    /// Envanter işlemleri iş mantığı sınıfı
    /// </summary>
    public class InventoryService : BaseService, IRollbackableAsync<int>, IDisposable, IDisposableInjections
    {
        /// <summary>
        /// Kaynakların serbest bırakılıp bırakılmadığı bilgisi
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// İçerisinde çalışılan servisin adı
        /// </summary>
        public override string ServiceName => "Services.Api.Business.Departments.IT.Services.InventoryService";

        /// <summary>
        /// Önbelleğe alınan envanterlerin önbellekteki adı
        /// </summary>
        private const string CACHED_INVENTORIES_KEY = "Services.Api.Business.Departments.IT.Inventories";

        /// <summary>
        /// Servisin ait olduğu api servisinin adı
        /// </summary>
        public override string ApiServiceName => "Services.Api.Business.Departments.IT";

        /// <summary>
        /// Önbelleğe alınan varsayılan envanterlerin önbellekteki adı
        /// </summary>
        private const string CACHED_INVENTORIES_DEFAULTS_KEY = "Services.Api.Business.Departments.IT.InventoryDefaults";

        /// <summary>
        /// Rediste tutulan önbellek yönetimini sağlayan sınıf
        /// </summary>
        private readonly IDistrubutedCacheProvider _redisCacheDataProvider;

        /// <summary>
        /// Mapping işlemleri için mapper nesnesi
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// İşlem tablosu için repository sınıfı
        /// </summary>
        private readonly TransactionRepository _transactionRepository;

        /// <summary>
        /// İşlem öğesi tablosu için repository sınıfı
        /// </summary>
        private readonly TransactionItemRepository _transactionItemRepository;

        /// <summary>
        /// Envanter tablosu için repository sınıfı
        /// </summary>
        private readonly InventoryRepository _inventoryRepository;

        /// <summary>
        /// Varsayılan envanterler tablosu için repository sınıfı
        /// </summary>
        private readonly InventoryDefaultsRepository _inventoryDefaultsRepository;

        /// <summary>
        /// Çalışan envanterleri tablosu için repository sınıfı
        /// </summary>
        private readonly WorkerInventoryRepository _workerInventoryRepository;

        /// <summary>
        /// Çalışanlara verilecek stoğu olmayan envanterler tablosu nesnesi
        /// </summary>
        private readonly PendingWorkerInventoryRepository _pendingWorkerInventoryRepository;

        /// <summary>
        /// Satınalma departmanına tükenen envanter için alım talebi kuyruğuna kayıt ekleyecek nesne
        /// </summary>
        private readonly CreateInventoryRequestPublisher _createInventoryRequestPublisher;

        /// <summary>
        /// Veritabanı iş birimi nesnesi
        /// </summary>
        private readonly ISqlUnitOfWork _unitOfWork;

        /// <summary>
        /// Dil çeviri sağlayıcısı sınıf
        /// </summary>
        private readonly TranslationProvider _translationProvider;

        /// <summary>
        /// Envanter işlemleri iş mantığı sınıfı
        /// </summary>
        /// <param name="mapper">Mapping işlemleri için mapper nesnesi</param>
        /// <param name="unitOfWork">Veritabanı iş birimi nesnesi</param>
        /// <param name="translationProvider">Dil çeviri sağlayıcısı sınıf</param>
        /// <param name="redisCacheDataProvider">Rediste tutulan önbellek yönetimini sağlayan sınıf</param>
        /// <param name="createInventoryRequestPublisher">Satınalma departmanına tükenen envanter için alım talebi kuyruğuna kayıt ekleyecek nesne</param>
        /// <param name="inventoryRepository">Envanter tablosu için repository sınıfı</param>
        /// <param name="inventoryDefaultsRepository">Varsayılan envanterler tablosu için repository sınıfı</param>
        /// <param name="workerInventoryRepository">Çalışan envanterleri tablosu için repository sınıfı</param>
        /// <param name="pendingWorkerInventoryRepository">Çalışanlara verilecek stoğu olmayan envanterler tablosu nesnesi</param>
        public InventoryService(
            IMapper mapper,
            ISqlUnitOfWork unitOfWork,
            TranslationProvider translationProvider,
            IDistrubutedCacheProvider redisCacheDataProvider,
            CreateInventoryRequestPublisher createInventoryRequestPublisher,
            TransactionRepository transactionRepository,
            TransactionItemRepository transactionItemRepository,
            InventoryRepository inventoryRepository,
            InventoryDefaultsRepository inventoryDefaultsRepository,
            WorkerInventoryRepository workerInventoryRepository,
            PendingWorkerInventoryRepository pendingWorkerInventoryRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _redisCacheDataProvider = redisCacheDataProvider;
            _translationProvider = translationProvider;

            _createInventoryRequestPublisher = createInventoryRequestPublisher;

            _transactionRepository = transactionRepository;
            _transactionItemRepository = transactionItemRepository;

            _inventoryRepository = inventoryRepository;
            _inventoryDefaultsRepository = inventoryDefaultsRepository;
            _workerInventoryRepository = workerInventoryRepository;
            _pendingWorkerInventoryRepository = pendingWorkerInventoryRepository;
        }

        /// <summary>
        /// Satın alınmayı bekleyen envanterle ilgili çalışana envanter ataması yapar veya alımı erteler
        /// </summary>
        /// <param name="inventoryRequest">Envanter talebi nesnesi</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        [LogBeforeRuntimeAttr(nameof(InformInventoryRequestAsync))]
        [LogAfterRuntimeAttr(nameof(InformInventoryRequestAsync))]
        public async Task InformInventoryRequestAsync(ITInventoryRequestModel inventoryRequest, CancellationTokenSource cancellationTokenSource)
        {
            if (inventoryRequest.Revoked)
                await _inventoryRepository.IncreaseStockCountAsync(inventoryRequest.InventoryId, inventoryRequest.Amount, cancellationTokenSource);

            await CreateCheckpointAsync(
                rollback: new RollbackModel()
                {
                    TransactionDate = DateTime.UtcNow,
                    TransactionIdentity = TransactionIdentity,
                    TransactionType = TransactionType.Update,
                    RollbackItems = new List<RollbackItemModel>
                    {
                        new RollbackItemModel
                        {
                            DataSet = InventoryRepository.TABLE_NAME,
                            Identity =inventoryRequest.InventoryId,
                            Name = nameof(InventoryEntity.CurrentStockCount),
                            Difference = inventoryRequest.Amount,
                            RollbackType= RollbackType.DecreaseValue
                        }
                    }
                },
                cancellationTokenSource: cancellationTokenSource);

            List<PendingWorkerInventoryEntity> pendingInventories = await _pendingWorkerInventoryRepository.GetListAsync(cancellationTokenSource);

            foreach (var pendingWorkerInventory in pendingInventories.Where(x => x.InventoryId == inventoryRequest.InventoryId).ToList())
            {
                if (inventoryRequest.Revoked && inventoryRequest.Amount > 0)
                {
                    int createdWorkerInventoryId = await _workerInventoryRepository.CreateAsync(
                        workerInventory: new WorkerInventoryEntity
                        {
                            FromDate = pendingWorkerInventory.FromDate,
                            ToDate = pendingWorkerInventory.ToDate,
                            WorkerId = pendingWorkerInventory.WorkerId,
                            InventoryId = pendingWorkerInventory.InventoryId
                        },
                        cancellationTokenSource: cancellationTokenSource);

                    await CreateCheckpointAsync(
                        rollback: new RollbackModel()
                        {
                            TransactionDate = DateTime.UtcNow,
                            TransactionIdentity = TransactionIdentity,
                            TransactionType = TransactionType.Insert,
                            RollbackItems = new List<RollbackItemModel>
                            {
                                new RollbackItemModel
                                {
                                    DataSet = WorkerInventoryRepository.TABLE_NAME,
                                    Identity = createdWorkerInventoryId,
                                    RollbackType= RollbackType.Delete
                                }
                            }
                        },
                        cancellationTokenSource: cancellationTokenSource);

                    await _inventoryRepository.DescendStockCountAsync(inventoryRequest.InventoryId, 1, cancellationTokenSource);

                    await CreateCheckpointAsync(
                        rollback: new RollbackModel()
                        {
                            TransactionDate = DateTime.UtcNow,
                            TransactionIdentity = TransactionIdentity,
                            TransactionType = TransactionType.Update,
                            RollbackItems = new List<RollbackItemModel>
                            {
                                new RollbackItemModel
                                {
                                    DataSet = InventoryRepository.TABLE_NAME,
                                    Identity = createdWorkerInventoryId,
                                    Name = nameof(InventoryEntity.CurrentStockCount),
                                    Difference = 1,
                                    RollbackType= RollbackType.IncreaseValue
                                }
                            }
                        },
                        cancellationTokenSource: cancellationTokenSource);

                    await _pendingWorkerInventoryRepository.SetCompleteAsync(pendingWorkerInventory.WorkerId, pendingWorkerInventory.InventoryId, cancellationTokenSource);

                    inventoryRequest.Amount -= 1;
                }
                else if (!inventoryRequest.Revoked)
                {
                    await _pendingWorkerInventoryRepository.DelayAsync(pendingWorkerInventory.WorkerId, pendingWorkerInventory.InventoryId, DateTime.UtcNow.AddDays(7), cancellationTokenSource);
                }
            }

            await _unitOfWork.SaveAsync(cancellationTokenSource);

            _redisCacheDataProvider.RemoveObject(CACHED_INVENTORIES_KEY);
        }

        /// <summary>
        /// Envanterlerin listesini verir
        /// </summary>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        [LogBeforeRuntimeAttr(nameof(GetInventoriesAsync))]
        [LogAfterRuntimeAttr(nameof(GetInventoriesAsync))]
        public async Task<List<ITInventoryModel>> GetInventoriesAsync(CancellationTokenSource cancellationTokenSource)
        {
            if (_redisCacheDataProvider.TryGetValue(CACHED_INVENTORIES_KEY, out List<ITInventoryModel> cachedInventories)
              &&
              cachedInventories != null && cachedInventories.Any())
            {
                return cachedInventories;
            }

            List<InventoryEntity> inventories = await _inventoryRepository.GetListAsync(cancellationTokenSource);

            List<ITInventoryModel> mappedInventories =
                _mapper.Map<List<InventoryEntity>, List<ITInventoryModel>>(inventories);

            _redisCacheDataProvider.Set(CACHED_INVENTORIES_KEY, mappedInventories);

            return mappedInventories;
        }

        /// <summary>
        /// Yeni envanter oluşturur
        /// </summary>
        /// <param name="inventory">Oluşturulacak envanter nesnesi</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        [LogBeforeRuntimeAttr(nameof(CreateInventoryAsync))]
        [LogAfterRuntimeAttr(nameof(CreateInventoryAsync))]
        public async Task<int> CreateInventoryAsync(ITInventoryModel inventory, CancellationTokenSource cancellationTokenSource)
        {
            InventoryEntity mappedInventory = _mapper.Map<ITInventoryModel, InventoryEntity>(inventory);

            int createdInventoryId = await _inventoryRepository.CreateAsync(mappedInventory, cancellationTokenSource);

            await CreateCheckpointAsync(
                rollback: new RollbackModel()
                {
                    TransactionType = TransactionType.Insert,
                    TransactionDate = DateTime.UtcNow,
                    TransactionIdentity = TransactionIdentity,
                    RollbackItems = new List<RollbackItemModel>
                    {
                        new RollbackItemModel()
                        {
                            Identity = createdInventoryId,
                            DataSet = InventoryRepository.TABLE_NAME,
                            RollbackType = RollbackType.Delete
                        }
                    }
                },
                cancellationTokenSource: cancellationTokenSource);

            await _unitOfWork.SaveAsync(cancellationTokenSource);

            inventory.Id = createdInventoryId;

            if (_redisCacheDataProvider.TryGetValue(CACHED_INVENTORIES_KEY, out List<ITInventoryModel> cachedInventories) && cachedInventories != null)
            {
                cachedInventories.Add(inventory);

                _redisCacheDataProvider.Set(CACHED_INVENTORIES_KEY, cachedInventories);
            }

            return createdInventoryId;
        }

        /// <summary>
        /// Yeni çalışan için varsayılan envanter ataması yapar
        /// </summary>
        /// <param name="inventory">Atanacak envanter</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        [LogBeforeRuntimeAttr(nameof(CreateDefaultInventoryForNewWorkerAsync))]
        [LogAfterRuntimeAttr(nameof(CreateDefaultInventoryForNewWorkerAsync))]
        public async Task CreateDefaultInventoryForNewWorkerAsync(ITDefaultInventoryForNewWorkerModel inventory, CancellationTokenSource cancellationTokenSource)
        {
            List<ITInventoryModel> existingInventories = await GetInventoriesAsync(cancellationTokenSource);

            if (!existingInventories.Any(x => x.Id == inventory.Id))
            {
                throw new Exception("Id ye ait envanter bulunamadı");
            }

            if (await _inventoryDefaultsRepository.CheckExistAsync(inventory.Id, cancellationTokenSource))
            {
                throw new Exception("Bu envanter zaten atanmış");
            }

            int createdInventoryDefault = await _inventoryDefaultsRepository.CreateAsync(
                 inventoryDefault: new InventoryDefaultsEntity()
                 {
                     InventoryId = inventory.Id,
                     ForNewWorker = true
                 },
                 cancellationTokenSource: cancellationTokenSource);

            await CreateCheckpointAsync(
                rollback: new RollbackModel()
                {
                    TransactionDate = DateTime.UtcNow,
                    TransactionIdentity = TransactionIdentity,
                    TransactionType = TransactionType.Insert,
                    RollbackItems = new List<RollbackItemModel>
                    {
                        new RollbackItemModel()
                        {
                            Identity = createdInventoryDefault,
                            DataSet = InventoryDefaultsRepository.TABLE_NAME,
                            RollbackType = RollbackType.Delete
                        }
                    }
                },
                cancellationTokenSource: cancellationTokenSource);

            await _unitOfWork.SaveAsync(cancellationTokenSource);

            if (_redisCacheDataProvider.TryGetValue(CACHED_INVENTORIES_DEFAULTS_KEY, out List<ITInventoryModel> cachedInventories)
                &&
                cachedInventories != null)
            {
                cachedInventories.Add(existingInventories.FirstOrDefault(x => x.Id == inventory.Id));

                _redisCacheDataProvider.Set(CACHED_INVENTORIES_DEFAULTS_KEY, cachedInventories);
            }
        }

        /// <summary>
        /// Yeni çalışanlara verilecek envanterlerin listesini verir
        /// </summary>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        [LogBeforeRuntimeAttr(nameof(GetInventoriesForNewWorker))]
        [LogAfterRuntimeAttr(nameof(GetInventoriesForNewWorker))]
        public List<ITDefaultInventoryForNewWorkerModel> GetInventoriesForNewWorker(CancellationTokenSource cancellationTokenSource)
        {
            if (_redisCacheDataProvider.TryGetValue(CACHED_INVENTORIES_DEFAULTS_KEY, out List<ITDefaultInventoryForNewWorkerModel> cachedInventories)
                &&
                cachedInventories != null && cachedInventories.Any())
            {
                return cachedInventories;
            }

            Task<List<InventoryEntity>> inventoriesTask = _inventoryRepository.GetListAsync(cancellationTokenSource);
            Task<List<InventoryDefaultsEntity>> inventoryDefaultsTask = _inventoryDefaultsRepository.GetListAsync(cancellationTokenSource);

            Task.WaitAll(new Task[] { inventoriesTask, inventoryDefaultsTask }, cancellationTokenSource.Token);

            List<ITDefaultInventoryForNewWorkerModel> inventories =
                (from inv in inventoriesTask.Result
                 join def in inventoryDefaultsTask.Result
                 on
                 inv.Id equals def.InventoryId
                 where
                 def.ForNewWorker
                 select
                 new ITDefaultInventoryForNewWorkerModel()
                 {
                     Id = inv.Id,
                     Name = inv.Name,
                     Amount = 1
                 }).ToList();

            _redisCacheDataProvider.Set(CACHED_INVENTORIES_DEFAULTS_KEY, inventories, DateTime.UtcNow.AddMinutes(10));

            return inventories;
        }

        /// <summary>
        /// Bir çalışana envanter ataması yapar
        /// </summary>
        /// <param name="worker">Envanter bilgisini içeren çalışan nesnesi</param>
        /// <param name="cancellationTokenSource"></param>
        /// <returns></returns>
        [LogBeforeRuntimeAttr(nameof(AssignInventoryToWorkerAsync))]
        [LogAfterRuntimeAttr(nameof(AssignInventoryToWorkerAsync))]
        public async Task AssignInventoryToWorkerAsync(List<ITAssignInventoryToWorkerModel> inventoryModel, CancellationTokenSource cancellationTokenSource)
        {
            List<int> inventoryIds = inventoryModel.Select(x => x.InventoryId).ToList();

            List<InventoryEntity> inventories =
                await _inventoryRepository.GetForSpecificIdAsync(inventoryIds, cancellationTokenSource);

            foreach (var workerModel in inventoryModel)
            {
                if (!inventories.Select(x => x.Id).Contains(workerModel.InventoryId))
                {
                    throw new Exception($"{workerModel} Id değerine sahip envanter bulunamadı");
                }

                InventoryEntity inventoryEntity = inventories.FirstOrDefault(x => x.Id == workerModel.InventoryId);

                if (inventoryEntity.CurrentStockCount <= 0)
                {
                    _createInventoryRequestPublisher.AddToBuffer(new InventoryRequestQueueModel
                    {
                        Amount = 3,
                        DepartmentId = (int)Constants.Departments.AdministrativeAffairs,
                        InventoryId = workerModel.InventoryId,
                        TransactionIdentity = TransactionIdentity,
                        GeneratedBy = ApiServiceName
                    });

                    inventories.FirstOrDefault(x => x.Id == workerModel.InventoryId).CurrentStockCount = 0;
                }
                else
                {
                    await _inventoryRepository.DescendStockCountAsync(workerModel.InventoryId, workerModel.Amount, cancellationTokenSource);

                    await CreateCheckpointAsync(
                        rollback: new RollbackModel()
                        {
                            TransactionDate = DateTime.UtcNow,
                            TransactionIdentity = TransactionIdentity,
                            TransactionType = TransactionType.Update,
                            RollbackItems = new List<RollbackItemModel>
                            {
                                new RollbackItemModel
                                {
                                    Identity = workerModel,
                                    Name = nameof(InventoryEntity.CurrentStockCount),
                                    DataSet = InventoryRepository.TABLE_NAME,
                                    RollbackType =  RollbackType.IncreaseValue,
                                    Difference = 1
                                }
                            }
                        },
                        cancellationTokenSource: cancellationTokenSource);

                    _redisCacheDataProvider.RemoveObject(CACHED_INVENTORIES_KEY);
                }

                if (inventories.FirstOrDefault(x => x.Id == workerModel.InventoryId).CurrentStockCount > 0)
                {
                    int createdWorkerInventoryId = await _workerInventoryRepository.CreateAsync(new WorkerInventoryEntity
                    {
                        FromDate = workerModel.FromDate,
                        ToDate = workerModel.ToDate,
                        InventoryId = workerModel.InventoryId,
                        WorkerId = workerModel.WorkerId
                    }, cancellationTokenSource);

                    await CreateCheckpointAsync(
                        rollback: new RollbackModel()
                        {
                            TransactionIdentity = TransactionIdentity,
                            TransactionDate = DateTime.UtcNow,
                            TransactionType = TransactionType.Insert,
                            RollbackItems = new List<RollbackItemModel>
                            {
                                new RollbackItemModel
                                {
                                    Identity = createdWorkerInventoryId,
                                    DataSet = WorkerInventoryRepository.TABLE_NAME,
                                    RollbackType = RollbackType.Delete
                                }
                            }
                        },
                        cancellationTokenSource: cancellationTokenSource);

                    inventories.FirstOrDefault(x => x.Id == workerModel.InventoryId).CurrentStockCount -= 1;
                }
                else
                {
                    int createdPendingWorkerInventoryId = await _pendingWorkerInventoryRepository.CreateAsync(new PendingWorkerInventoryEntity()
                    {
                        FromDate = workerModel.FromDate,
                        InventoryId = workerModel.InventoryId,
                        StockCount = 1,
                        ToDate = workerModel.ToDate,
                        WorkerId = workerModel.WorkerId
                    }, cancellationTokenSource);

                    await CreateCheckpointAsync(
                        rollback: new RollbackModel()
                        {
                            TransactionIdentity = TransactionIdentity,
                            TransactionDate = DateTime.UtcNow,
                            TransactionType = TransactionType.Insert,
                            RollbackItems = new List<RollbackItemModel>
                            {
                                new RollbackItemModel
                                {
                                    Identity = createdPendingWorkerInventoryId,
                                    DataSet = PendingWorkerInventoryRepository.TABLE_NAME,
                                    RollbackType = RollbackType.Delete
                                }
                            }
                        },
                        cancellationTokenSource: cancellationTokenSource);
                }
            }

            await _unitOfWork.SaveAsync(cancellationTokenSource);

            await _createInventoryRequestPublisher.PublishBufferAsync(cancellationTokenSource);
        }

        /// <summary>
        /// Kaynakları serbest bırakır
        /// </summary>
        /// <param name="disposing">Kaynakların serbest bırakılıp bırakılmadığı bilgisi</param>
        public override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!disposed)
                {
                    disposed = true;
                }
            }
        }

        /// <summary>
        /// Bir işlemi geri almak için yedekleme noktası oluşturur
        /// </summary>
        /// <param name="rollback">İşlemin yedekleme noktası nesnesi</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns>TIdentity işlemin geri dönüş tipidir</returns>
        [LogBeforeRuntimeAttr(nameof(CreateCheckpointAsync))]
        [LogAfterRuntimeAttr(nameof(CreateCheckpointAsync))]
        public async Task<int> CreateCheckpointAsync(RollbackModel rollback, CancellationTokenSource cancellationTokenSource)
        {
            RollbackEntity rollbackEntity = _mapper.Map<RollbackModel, RollbackEntity>(rollback);

            List<RollbackItemEntity> rollbackItemEntities = _mapper.Map<List<RollbackItemModel>, List<RollbackItemEntity>>(rollback.RollbackItems);

            foreach (var rollbackItemEntity in rollbackItemEntities)
            {
                rollbackItemEntity.TransactionIdentity = rollbackEntity.TransactionIdentity;

                await _transactionItemRepository.CreateAsync(rollbackItemEntity, cancellationTokenSource);
            }

            return await _transactionRepository.CreateAsync(rollbackEntity, cancellationTokenSource);
        }

        /// <summary>
        /// Bir işlemi geri alır
        /// </summary>
        /// <param name="rollback">Geri alınacak işlemin yedekleme noktası nesnesi</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns>TIdentity işlemin geri dönüş tipidir</returns>
        [LogBeforeRuntimeAttr(nameof(RollbackTransactionAsync))]
        [LogAfterRuntimeAttr(nameof(RollbackTransactionAsync))]
        public async Task<int> RollbackTransactionAsync(RollbackModel rollback, CancellationTokenSource cancellationTokenSource)
        {
            foreach (var rollbackItem in rollback.RollbackItems)
            {
                switch (rollbackItem.DataSet?.ToString())
                {
                    case InventoryRepository.TABLE_NAME:
                        if (rollbackItem.RollbackType == RollbackType.Delete)
                        {
                            await _inventoryRepository.DeleteAsync((int)rollbackItem.Identity, cancellationTokenSource);
                        }
                        else if (rollbackItem.RollbackType == RollbackType.Insert)
                        {
                            await _inventoryRepository.UnDeleteAsync((int)rollbackItem.Identity, cancellationTokenSource);
                        }
                        else if (rollbackItem.RollbackType == RollbackType.Update)
                        {
                            await _inventoryRepository.SetAsync((int)rollbackItem.Identity, rollbackItem.Name, rollbackItem.OldValue, cancellationTokenSource);
                        }
                        else if (rollbackItem.RollbackType == RollbackType.IncreaseValue)
                        {
                            await _inventoryRepository.IncreaseStockCountAsync((int)rollbackItem.Identity, (int)rollbackItem.Difference, cancellationTokenSource);
                        }
                        else if (rollbackItem.RollbackType == RollbackType.DecreaseValue)
                        {
                            await _inventoryRepository.DescendStockCountAsync((int)rollbackItem.Identity, (int)rollbackItem.Difference, cancellationTokenSource);
                        }
                        else
                            throw new Exception(
                                await _translationProvider.TranslateAsync("Tanimsiz.Geri.Alma", Region, cancellationToken: cancellationTokenSource.Token));
                        break;
                    case InventoryDefaultsRepository.TABLE_NAME:
                        if (rollbackItem.RollbackType == RollbackType.Delete)
                        {
                            await _inventoryDefaultsRepository.DeleteAsync((int)rollbackItem.Identity, cancellationTokenSource);
                        }
                        else if (rollbackItem.RollbackType == RollbackType.Insert)
                        {
                            await _inventoryDefaultsRepository.UnDeleteAsync((int)rollbackItem.Identity, cancellationTokenSource);
                        }
                        else if (rollbackItem.RollbackType == RollbackType.Update)
                        {
                            await _inventoryDefaultsRepository.SetAsync((int)rollbackItem.Identity, rollbackItem.Name, rollbackItem.OldValue, cancellationTokenSource);
                        }
                        else
                            throw new Exception(
                                 await _translationProvider.TranslateAsync("Tanimsiz.Geri.Alma", Region, cancellationToken: cancellationTokenSource.Token));
                        break;
                    case WorkerInventoryRepository.TABLE_NAME:
                        if (rollbackItem.RollbackType == RollbackType.Delete)
                        {
                            await _workerInventoryRepository.DeleteAsync((int)rollbackItem.Identity, cancellationTokenSource);
                        }
                        else if (rollbackItem.RollbackType == RollbackType.Insert)
                        {
                            await _inventoryDefaultsRepository.UnDeleteAsync((int)rollbackItem.Identity, cancellationTokenSource);
                        }
                        else if (rollbackItem.RollbackType == RollbackType.Update)
                        {
                            await _inventoryDefaultsRepository.SetAsync((int)rollbackItem.Identity, rollbackItem.Name, rollbackItem.OldValue, cancellationTokenSource);
                        }
                        else
                            throw new Exception(
                                await _translationProvider.TranslateAsync("Tanimsiz.Geri.Alma", Region, cancellationToken: cancellationTokenSource.Token));
                        break;
                    default:
                        break;
                }
            }

            int rollbackResult = await _transactionRepository.SetRolledbackAsync(rollback.TransactionIdentity, cancellationTokenSource);

            await _unitOfWork.SaveAsync(cancellationTokenSource);

            return rollbackResult;
        }

        public void DisposeInjections()
        {
            _redisCacheDataProvider.Dispose();
            _inventoryRepository.Dispose();
            _inventoryDefaultsRepository.Dispose();
            _pendingWorkerInventoryRepository.Dispose();
            _transactionItemRepository.Dispose();
            _transactionRepository.Dispose();
            _workerInventoryRepository.Dispose();
            _unitOfWork.Dispose();
            _translationProvider.Dispose();
        }
    }
}
