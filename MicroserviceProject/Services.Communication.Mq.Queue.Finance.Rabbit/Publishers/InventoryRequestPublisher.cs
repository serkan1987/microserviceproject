﻿using Services.Communication.Mq.Queue.Finance.Configuration;
using Services.Communication.Mq.Queue.Finance.Models;
using Services.Communication.Mq.Rabbit.Publisher;

namespace Services.Communication.Mq.Queue.Finance.Rabbit.Publishers
{
    /// <summary>
    /// Satınalma departmanından alınması istenilen envanter talepleri için kayıt açar
    /// </summary>
    public class InventoryRequestPublisher : BasePublisher<DecidedCostQueueModel>, IDisposable
    {
        /// <summary>
        /// Kaynakların serbest bırakılıp bırakılmadığı bilgisi
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Satınalma departmanından alınması istenilen envanter talepleri için kayıt açar
        /// </summary>
        /// <param name="rabbitConfiguration">Kuyruk ayarlarını verece configuration nesnesi</param>
        public InventoryRequestPublisher(
            InventoryRequestRabbitConfiguration rabbitConfiguration)
            : base(rabbitConfiguration)
        {

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
