﻿using Infrastructure.Communication.Mq.Rabbit;

using Services.Communication.Http.Broker.Department.AA.Abstract;
using Services.Communication.Http.Broker.Department.AA.CQRS.Commands.Requests;
using Services.Communication.Mq.Queue.AA.Configuration;
using Services.Communication.Mq.Queue.AA.Models;

namespace Services.Communication.Mq.Queue.AA.Rabbit.Consumers
{
    /// <summary>
    /// Envanter talebiyle ilgili satınalma sonucunu tüketen sınıf
    /// </summary>
    public class AAInformInventoryRequestConsumer : IDisposable
    {
        /// <summary>
        /// Kaynakların serbest bırakılıp bırakılmadığı bilgisi
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Rabbit kuyruğuyla iletişim kuracak tüketici sınıf
        /// </summary>
        private readonly Consumer<AAInventoryRequestQueueModel> _consumer;

        /// <summary>
        /// İdari işler servis iletişimcisi
        /// </summary>
        private readonly IAACommunicator _aaCommunicator;

        /// <summary>
        /// Envanter talebiyle ilgili satınalma sonucunu tüketen sınıf
        /// </summary>
        /// <param name="rabbitConfiguration">Kuyruk ayarlarının alınacağın configuration nesnesi</param>
        /// <param name="aaCommunicator">İdari işler servis iletişimcisi</param>
        public AAInformInventoryRequestConsumer(
            AAInformInventoryRequestRabbitConfiguration rabbitConfiguration,
            IAACommunicator aaCommunicator)
        {
            _aaCommunicator = aaCommunicator;

            _consumer = new Consumer<AAInventoryRequestQueueModel>(rabbitConfiguration);
            _consumer.OnConsumed += Consumer_OnConsumed;
        }

        private async Task Consumer_OnConsumed(AAInventoryRequestQueueModel data)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            Http.Broker.Department.AA.Models.AAInventoryRequestModel inventoryRequestModel =
                new Http.Broker.Department.AA.Models.AAInventoryRequestModel
                {
                    Amount = data.Amount,
                    Done = data.Done,
                    InventoryId = data.InventoryId,
                    Revoked = data.Revoked
                };

            await _aaCommunicator.InformInventoryRequestAsync(new AAInformInventoryRequestCommandRequest()
            {
                InventoryRequest = inventoryRequestModel
            }, data?.TransactionIdentity, cancellationTokenSource);
        }

        /// <summary>
        /// Kayıtları yakalamaya başlar
        /// </summary>
        public async Task StartToConsumeAsync(CancellationTokenSource cancellationTokenSource)
        {
            await _consumer.StartConsumeAsync(cancellationTokenSource);
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
                    _aaCommunicator.Dispose();
                }

                disposed = true;
            }
        }
    }
}
