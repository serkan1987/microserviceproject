﻿
using Microsoft.Extensions.Configuration;

using Services.Communication.Mq.Configuration;

namespace Services.Communication.Mq.Queue.Buying.Configuration
{
    /// <summary>
    /// Satın alınması planlanan envanterlere ait bütçenin sonuçlandırılması için yapılandırma sınıfı
    /// </summary>
    public class NotifyCostApprovementRabbitConfiguration : BaseConfiguration, IDisposable
    {
        /// <summary>
        /// Kaynakların serbest bırakılıp bırakılmadığı bilgisi
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Satın alınması planlanan envanterlere ait bütçenin sonuçlandırılması için yapılandırma sınıfı
        /// <paramref name="configuration">Ayarların okunacağı configuration nesnesi</paramref>
        /// </summary>
        /// <param name="configuration"></param>
        public NotifyCostApprovementRabbitConfiguration(IConfiguration configuration)
            : base(configuration)
        {
            QueueName = "buying.queue.cost.notifycostapprovement";
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
