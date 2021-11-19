﻿using Communication.Mq.Rabbit.Configuration.Department.AA;
using Communication.Mq.Rabbit.Publisher.Department.AA.Models;

using System;

namespace Communication.Mq.Rabbit.Publisher.Department.AA
{
    /// <summary>
    /// İdari işler departmanına satın alımla ilgili olumlu veya olumsuz dönüş verisini rabbit kuyruğuna ekler
    /// </summary>
    public class AAInformInventoryRequestPublisher : BasePublisher<InventoryRequestModel>, IDisposable
    {
        /// <summary>
        /// Kaynakların serbest bırakılıp bırakılmadığı bilgisi
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// İdari işler departmanına satın alımla ilgili olumlu veya olumsuz dönüş verisini rabbit kuyruğuna ekler
        /// </summary>
        /// <param name="rabbitConfiguration">Kuyruk ayarlarını verece configuration nesnesi</param>
        public AAInformInventoryRequestPublisher(
            AAInformInventoryRequestRabbitConfiguration rabbitConfiguration)
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
