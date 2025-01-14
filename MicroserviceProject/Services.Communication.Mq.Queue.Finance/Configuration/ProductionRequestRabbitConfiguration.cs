﻿
using Microsoft.Extensions.Configuration;

using Services.Communication.Mq.Configuration;

namespace Services.Communication.Mq.Queue.Finance.Configuration
{
    /// <summary>
    /// Finans departmanına üretilmesi istenilen ürünler için talep açacak yapılandırma sınıfı
    /// </summary>
    public class ProductionRequestRabbitConfiguration : BaseConfiguration, IDisposable
    {
        /// <summary>
        /// Kaynakların serbest bırakılıp bırakılmadığı bilgisi
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Finans departmanına üretilmesi istenilen ürünler için talep açacak yapılandırma sınıfı
        /// <paramref name="configuration">Ayarların okunacağı configuration nesnesi</paramref>
        /// </summary>
        /// <param name="configuration"></param>
        public ProductionRequestRabbitConfiguration(IConfiguration configuration)
            : base(configuration)
        {
            QueueName = "finance.queue.request.productionrequest";
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
    }
}
