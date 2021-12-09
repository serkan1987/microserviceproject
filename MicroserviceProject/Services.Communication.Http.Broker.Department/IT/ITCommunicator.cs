﻿using Infrastructure.Communication.Http.Broker;
using Infrastructure.Communication.Http.Models;
using Infrastructure.Routing.Providers;

using Services.Communication.Http.Broker.Department.IT.Models;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Communication.Http.Broker.Department.IT
{
    /// <summary>
    /// IT servisi için iletişim kurucu sınıf
    /// </summary>
    public class ITCommunicator : IDisposable
    {
        /// <summary>
        /// Kaynakların serbest bırakılıp bırakılmadığı bilgisi
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Servis rotalarına ait endpoint isimlerini sağlayan sınıfın nesnesi
        /// </summary>
        private readonly RouteNameProvider _routeNameProvider;

        /// <summary>
        /// Yetki denetimi destekli servis iletişim sağlayıcı sınıfın nesnesi
        /// </summary>
        private readonly ServiceCommunicator _serviceCommunicator;

        /// <summary>
        /// IT servisi için iletişim kurucu sınıf
        /// </summary>
        /// <param name="routeNameProvider">Servis rotalarına ait endpoint isimlerini sağlayan sınıfın nesnesi</param>
        /// <param name="serviceCommunicator">Yetki denetimi destekli servis iletişim sağlayıcı sınıfın nesnesi</param>
        public ITCommunicator(
            RouteNameProvider routeNameProvider,
            ServiceCommunicator serviceCommunicator)
        {
            _routeNameProvider = routeNameProvider;
            _serviceCommunicator = serviceCommunicator;
        }

        /// <summary>
        /// IT envanterlerini verir
        /// </summary>
        /// <param name="transactionIdentity">Servislerin işlem süreçleri boyunca izleyeceği işlem kimliği</param>
        /// <param name="cancellationTokenSource"></param>
        /// <returns></returns>
        public async Task<ServiceResultModel<List<InventoryModel>>> GetInventoriesAsync(string transactionIdentity, CancellationTokenSource cancellationTokenSource)
        {
            ServiceResultModel<List<InventoryModel>> serviceResult =
                    await
                    _serviceCommunicator.Call<List<InventoryModel>>(
                            serviceName: _routeNameProvider.IT_GetInventories,
                            postData: null,
                            queryParameters: null,
                            headers: new List<KeyValuePair<string, string>>()
                            {
                                new KeyValuePair<string, string>("TransactionIdentity", transactionIdentity)
                            },
                            cancellationTokenSource: cancellationTokenSource);

            return serviceResult;
        }

        /// <summary>
        /// IT envanteri oluşturur
        /// </summary>
        /// <<param name="inventoryModel">Envanter modeli</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        public async Task<ServiceResultModel<int>> CreateInventoryAsync(InventoryModel inventoryModel, CancellationTokenSource cancellationTokenSource)
        {
            return await _serviceCommunicator.Call<int>(
                serviceName: _routeNameProvider.IT_CreateInventory,
                postData: inventoryModel,
                queryParameters: null,
                headers: null,
                cancellationTokenSource: cancellationTokenSource);
        }

        /// <summary>
        /// Yeni çalışanlar için IT tarafından varsayılan envanterleri verir
        /// </summary>
        /// <param name="transactionIdentity">Servislerin işlem süreçleri boyunca izleyeceği işlem kimliği</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        public async Task<ServiceResultModel<List<InventoryModel>>> GetInventoriesForNewWorkerAsync(string transactionIdentity, CancellationTokenSource cancellationTokenSource)
        {
            ServiceResultModel<List<InventoryModel>> defaultInventoriesServiceResult =
                    await _serviceCommunicator.Call<List<InventoryModel>>(
                        serviceName: _routeNameProvider.IT_GetInventoriesForNewWorker,
                        postData: null,
                        queryParameters: null,
                        headers: new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>("TransactionIdentity", transactionIdentity)
                        },
                        cancellationTokenSource);

            return defaultInventoriesServiceResult;
        }

        /// <summary>
        /// Çalışana IT envanteri ataması yapar
        /// </summary>
        /// <param name="workerModel">Çalışan modeli</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        public async Task<ServiceResultModel<int>> AssignInventoryToWorkerAsync(WorkerModel workerModel, CancellationTokenSource cancellationTokenSource)
        {
            return await _serviceCommunicator.Call<int>(
                serviceName: _routeNameProvider.IT_AssignInventoryToWorker,
                postData: workerModel,
                queryParameters: null,
                headers: null,
                cancellationTokenSource: cancellationTokenSource);
        }

        /// <summary>
        /// Yeni çalışan için varsayılan IT envanteri ataması yapar
        /// </summary>
        /// <param name="inventoryModel">Envanter modeli</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        public async Task<ServiceResultModel<InventoryModel>> CreateDefaultInventoryForNewWorkerAsync(InventoryModel inventoryModel, CancellationTokenSource cancellationTokenSource)
        {
            return await _serviceCommunicator.Call<InventoryModel>(
                serviceName: _routeNameProvider.IT_CreateDefaultInventoryForNewWorker,
                postData: inventoryModel,
                queryParameters: null,
                headers: null,
                cancellationTokenSource: cancellationTokenSource);
        }

        /// <summary>
        /// Yeni çalışanlar için IT tarafından varsayılan envanterleri verir
        /// </summary>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        public async Task<ServiceResultModel<List<InventoryModel>>> GetInventoriesForNewWorkerAsync(CancellationTokenSource cancellationTokenSource)
        {
            return await _serviceCommunicator.Call<List<InventoryModel>>(
                serviceName: _routeNameProvider.IT_GetInventoriesForNewWorker,
                postData: null,
                queryParameters: null,
                headers: null,
                cancellationTokenSource: cancellationTokenSource);
        }

        /// <summary>
        /// Bilgi teknolojileri departmanının bekleyen satın alımları için bilgilendirme yapar
        /// </summary>
        /// <param name="inventoryRequestModel">Satın alım modeli</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        public async Task<ServiceResultModel<int>> InformInventoryRequestAsync(InventoryRequestModel inventoryRequestModel, CancellationTokenSource cancellationTokenSource)
        {
            return await _serviceCommunicator.Call<int>(
                serviceName: _routeNameProvider.IT_InformInventoryRequest,
                postData: inventoryRequestModel,
                queryParameters: null,
                headers: null,
                cancellationTokenSource: cancellationTokenSource);
        }

        /// <summary>
        /// İptal edilmesi istenilen bir session ın düşürülmesi talebini iletir
        /// </summary>
        /// <param name="tokenKey">Düşürülecek session a ait token</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        public async Task<ServiceResultModel> RemoveSessionIfExistsInCacheAsync(string tokenKey, CancellationTokenSource cancellationTokenSource)
        {
            ServiceResultModel serviceResult =
                await _serviceCommunicator.Call(
                    serviceName: _routeNameProvider.IT_RemoveSessionIfExistsInCache,
                    postData: null,
                    queryParameters: new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("tokenKey",tokenKey)
                    },
                    headers: null,
                    cancellationTokenSource);

            return serviceResult;
        }

        /// <summary>
        /// Kaynakları serbest bırakır
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Kaynakları serbest bırakır
        /// </summary>
        /// <param name="disposing">Kaynakların serbest bırakılıp bırakılmadığı bilgisi</param>
        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!disposed)
                {
                    _routeNameProvider.Dispose();
                    _serviceCommunicator.Dispose();
                }

                disposed = true;
            }
        }
    }
}