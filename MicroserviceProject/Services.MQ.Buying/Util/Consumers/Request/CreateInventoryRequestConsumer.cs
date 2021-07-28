﻿using Infrastructure.Communication.Broker;
using Infrastructure.Communication.Mq.Rabbit;
using Infrastructure.Communication.Mq.Rabbit.Configuration.Department.Buying;
using Communication.Mq.Rabbit.Publisher.Department.Buying.Models;
using Infrastructure.Routing.Providers;

using System;
using System.Threading;
using System.Threading.Tasks;
using Communication.Http.Department.Buying;

namespace Services.MQ.Buying.Util.Consumers.Request
{
    /// <summary>
    /// Satınalma departmanına alınması istenilen envanter taleplerini tüketen sınıf
    /// </summary>
    public class CreateInventoryRequestConsumer : IDisposable
    {
        /// <summary>
        /// Kaynakların serbest bırakılıp bırakılmadığı bilgisi
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Rabbit kuyruğuyla iletişim kuracak tüketici sınıf
        /// </summary>
        private readonly Consumer<InventoryRequestModel> _consumer;

        /// <summary>
        /// Satınalma departmanı servis iletişimcisi
        /// </summary>
        private readonly BuyingCommunicator _buyingCommunicator;

        /// <summary>
        /// Satınalma departmanına alınması istenilen envanter taleplerini tüketen sınıf
        /// </summary>
        /// <param name="rabbitConfiguration">Kuyruk ayarlarının alınacağın configuration nesnesi</param>
        /// <param name="buyingCommunicator">Satınalma departmanı servis iletişimcisi</param>
        public CreateInventoryRequestConsumer(
            CreateInventoryRequestRabbitConfiguration rabbitConfiguration,
            BuyingCommunicator buyingCommunicator)
        {
            _buyingCommunicator = buyingCommunicator;

            _consumer = new Consumer<InventoryRequestModel>(rabbitConfiguration);
            _consumer.OnConsumed += Consumer_OnConsumed;
        }

        private async Task Consumer_OnConsumed(InventoryRequestModel data)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            Communication.Http.Department.Buying.Models.InventoryRequestModel inventoryRequestModel = new Communication.Http.Department.Buying.Models.InventoryRequestModel
            {
                Amount = data.Amount,
                DepartmentId = data.DepartmentId,
                InventoryId = data.InventoryId
            };

            await _buyingCommunicator.CreateInventoryRequestAsync(inventoryRequestModel, cancellationTokenSource);
        }

        /// <summary>
        /// Kayıtları yakalamaya başlar
        /// </summary>
        public void StartToConsume()
        {
            _consumer.StartToConsume();
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
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!disposed)
                {
                    _consumer.Dispose();
                    _buyingCommunicator.Dispose();
                }

                disposed = true;
            }
        }
    }
}
