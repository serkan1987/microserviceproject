﻿using AutoMapper;

using Infrastructure.Caching.Redis;
using Infrastructure.Communication.Http.Wrapper;
using Infrastructure.Communication.Http.Wrapper.Disposing;
using Infrastructure.Localization.Providers;
using Infrastructure.Transaction.Recovery;
using Infrastructure.Transaction.UnitOfWork.EntityFramework;

using Services.Business.Departments.Selling.Configuration.Persistence;
using Services.Business.Departments.Selling.Entities.EntityFramework;
using Services.Business.Departments.Selling.Models;
using Services.Business.Departments.Selling.Repositories.EntityFramework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Business.Departments.Selling.Services
{
    /// <summary>
    /// Satışlar iş mantığı sınıfı
    /// </summary>
    public class SellingService : BaseService, IRollbackableAsync, IAsyncDisposable, IDisposableInjectionsAsync
    {
        /// <summary>
        /// Kaynakların serbest bırakılıp bırakılmadığı bilgisi
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// İçerisinde çalışılan servisin adı
        /// </summary>
        public override string ServiceName => "Services.Business.Departments.Services.Services.SellingService";

        /// <summary>
        /// Servisin ait olduğu api servisinin adı
        /// </summary>
        public override string ApiServiceName => "Services.Business.Departments.Selling";


        /// <summary>
        /// Rediste tutulan önbellek yönetimini sağlayan sınıf
        /// </summary>
        private readonly RedisCacheDataProvider _redisCacheDataProvider;

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
        /// Müşteriler repository sınıfı
        /// </summary>
        private readonly SellRepository _sellRepository;

        /// <summary>
        /// Veritabanı iş birimi nesnesi
        /// </summary>
        private readonly IUnitOfWork<SellingContext> _unitOfWork;

        /// <summary>
        /// Dil çeviri sağlayıcısı sınıf
        /// </summary>
        private readonly TranslationProvider _translationProvider;

        /// <summary>
        /// Satışlar iş mantığı sınıfı
        /// </summary>
        /// <param name="mapper">Mapping işlemleri için mapper nesnesi</param>
        /// <param name="unitOfWork">Veritabanı iş birimi nesnesi</param>
        /// <param name="translationProvider">Dil çeviri sağlayıcısı sınıf nesnesi</param>
        /// <param name="redisCacheDataProvider">Rediste tutulan önbellek yönetimini sağlayan sınıf nesnesi</param>
        /// <param name="transactionRepository">İşlem tablosu için repository sınıfı nesnesi</param>
        /// <param name="transactionItemRepository">İşlem öğesi tablosu için repository sınıfı nesnesi</param>
        /// <param name="sellRepository">Satışlar repository sınıfı nesnesi</param>
        public SellingService(
            IMapper mapper,
            IUnitOfWork<SellingContext> unitOfWork,
            TranslationProvider translationProvider,
            RedisCacheDataProvider redisCacheDataProvider,
            TransactionRepository transactionRepository,
            TransactionItemRepository transactionItemRepository,
            SellRepository sellRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _translationProvider = translationProvider;
            _redisCacheDataProvider = redisCacheDataProvider;

            _transactionRepository = transactionRepository;
            _transactionItemRepository = transactionItemRepository;
            _sellRepository = sellRepository;
        }

        /// <summary>
        /// Bir işlemi geri almak için yedekleme noktası oluşturur
        /// </summary>
        /// <param name="rollback">İşlemin yedekleme noktası nesnesi</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns>TIdentity işlemin geri dönüş tipidir</returns>
        public async Task CreateCheckpointAsync(RollbackModel rollback, CancellationTokenSource cancellationTokenSource)
        {
            RollbackEntity rollbackEntity = _mapper.Map<RollbackModel, RollbackEntity>(rollback);

            List<RollbackItemEntity> rollbackItemEntities = _mapper.Map<List<RollbackItemModel>, List<RollbackItemEntity>>(rollback.RollbackItems);

            foreach (var rollbackItemEntity in rollbackItemEntities)
            {
                rollbackItemEntity.TransactionIdentity = rollbackEntity.TransactionIdentity;

                await _transactionItemRepository.CreateAsync(rollbackItemEntity, cancellationTokenSource);
            }

            await _transactionRepository.CreateAsync(rollbackEntity, cancellationTokenSource);
        }

        /// <summary>
        /// Bir işlemi geri alır
        /// </summary>
        /// <param name="rollback">Geri alınacak işlemin yedekleme noktası nesnesi</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns>TIdentity işlemin geri dönüş tipidir</returns>
        public async Task RollbackTransactionAsync(RollbackModel rollback, CancellationTokenSource cancellationTokenSource)
        {
            foreach (var rollbackItem in rollback.RollbackItems)
            {
                switch (rollbackItem.DataSet?.ToString())
                {
                    case SellRepository.TABLE_NAME:
                        if (rollbackItem.RollbackType == RollbackType.Delete)
                        {
                            await _sellRepository.DeleteAsync((int)rollbackItem.Identity, cancellationTokenSource);
                        }
                        else if (rollbackItem.RollbackType == RollbackType.Insert)
                        {
                            await _sellRepository.UnDeleteAsync((int)rollbackItem.Identity, cancellationTokenSource);
                        }
                        else if (rollbackItem.RollbackType == RollbackType.Update)
                        {
                            await _sellRepository.SetAsync((int)rollbackItem.Identity, rollbackItem.Name, rollbackItem.OldValue, cancellationTokenSource);
                        }
                        else
                            throw new Exception(
                                await _translationProvider.TranslateAsync("Tanimsiz.Geri.Alma", Region, cancellationToken: cancellationTokenSource.Token));
                        break;
                    default:
                        break;
                }
            }

            await _transactionRepository.SetRolledbackAsync(rollback.TransactionIdentity, cancellationTokenSource);

            await _unitOfWork.SaveAsync(cancellationTokenSource);
        }

        public async Task DisposeInjectionsAsync()
        {
            _redisCacheDataProvider.Dispose();
            await _sellRepository.DisposeAsync();
            await _transactionItemRepository.DisposeAsync();
            await _transactionRepository.DisposeAsync();
            await _unitOfWork.DisposeAsync();
            _translationProvider.Dispose();
        }

        /// <summary>
        /// Kaynakları serbest bırakır
        /// </summary>
        /// <returns></returns>
        public ValueTask DisposeAsync()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

            return ValueTask.CompletedTask;
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
        /// Bir satış işlemini kayıt altına alır
        /// </summary>
        /// <param name="sellModel">Satış modeli</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        public async Task<int> CreateSellingAsync(SellModel sellModel, CancellationTokenSource cancellationTokenSource)
        {
            SellEntity mappedSellEntity = _mapper.Map<SellModel, SellEntity>(sellModel);

            await _sellRepository.CreateAsync(mappedSellEntity, cancellationTokenSource);

            await CreateCheckpointAsync(
                rollback: new RollbackModel()
                {
                    TransactionType = TransactionType.Insert,
                    TransactionDate = DateTime.Now,
                    TransactionIdentity = TransactionIdentity,
                    RollbackItems = new List<RollbackItemModel>
                    {
                        new RollbackItemModel()
                        {
                            Identity = mappedSellEntity.Id,
                            DataSet = SellRepository.TABLE_NAME,
                            RollbackType = RollbackType.Delete
                        }
                    }
                },
                cancellationTokenSource: cancellationTokenSource);

            await _unitOfWork.SaveAsync(cancellationTokenSource);

            return mappedSellEntity.Id;
        }

        /// <summary>
        /// Kayıtlı satışların listesini verir
        /// </summary>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        public async Task<List<SellModel>> GetSoldsAsync(CancellationTokenSource cancellationTokenSource)
        {
            List<SellEntity> sells = await _sellRepository.GetListAsync(cancellationTokenSource);

            List<SellModel> mappedSells = _mapper.Map<List<SellEntity>, List<SellModel>>(sells);

            return mappedSells;
        }
    }
}