﻿using Infrastructure.Communication.Mq.Rabbit;

using Services.Communication.Http.Broker.Department.Finance.Abstract;
using Services.Communication.Http.Broker.Department.Finance.CQRS.Commands.Requests;
using Services.Communication.Http.Broker.Department.Finance.Models;
using Services.Communication.Mq.Queue.Finance.Configuration;
using Services.Communication.Mq.Queue.Finance.Models;

namespace Services.Communication.Mq.Queue.Finance.Rabbit.Consumers
{
    /// <summary>
    /// Finans departmanına üretilmesi istenilen ürünleri tüketen sınıf
    /// </summary>
    public class ProductionRequestConsumer : IDisposable
    {
        /// <summary>
        /// Kaynakların serbest bırakılıp bırakılmadığı bilgisi
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Rabbit kuyruğuyla iletişim kuracak tüketici sınıf
        /// </summary>
        private readonly Consumer<ProductionRequestQueueModel> _consumer;

        /// <summary>
        /// Finans departmanı servis iletişimcisi
        /// </summary>
        private readonly IFinanceCommunicator _financeCommunicator;

        /// <summary>
        /// Finans departmanına üretilmesi istenilen ürünleri tüketen sınıf
        /// </summary>
        /// <param name="rabbitConfiguration">Kuyruk ayarlarının alınacağın configuration nesnesi</param>
        /// <param name="financeCommunicator">Finans departmanı servis iletişimcisi</param>
        public ProductionRequestConsumer(
            ProductionRequestRabbitConfiguration rabbitConfiguration,
            IFinanceCommunicator financeCommunicator)
        {
            _financeCommunicator = financeCommunicator;
            _consumer = new Consumer<ProductionRequestQueueModel>(rabbitConfiguration);
            _consumer.OnConsumed += Consumer_OnConsumed;
        }

        private async Task Consumer_OnConsumed(ProductionRequestQueueModel data)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            ProductionRequestModel productionRequest = new ProductionRequestModel
            {
                Amount = data.Amount,
                DepartmentId = data.DepartmentId,
                ProductId = data.ProductId,
                ReferenceNumber = data.ReferenceNumber
            };

            await _financeCommunicator.CreateProductionRequestAsync(new CreateProductionRequestCommandRequest()
            {
                ProductionRequest = productionRequest,
            }, data?.TransactionIdentity, cancellationTokenSource);
        }

        /// <summary>
        /// Kayıtları yakalamaya başlar
        /// </summary>
        public async Task StartConsumeAsync(CancellationTokenSource cancellationTokenSource)
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
                    _financeCommunicator.Dispose();
                }

                disposed = true;
            }
        }
    }
}
