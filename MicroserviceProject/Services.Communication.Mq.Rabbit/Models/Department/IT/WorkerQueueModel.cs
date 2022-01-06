﻿using System.Collections.Generic;

namespace Services.Communication.Mq.Rabbit.Department.Models.IT
{
    /// <summary>
    /// Çalışanlar
    /// </summary>
    public class WorkerQueueModel : BaseQueueModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Çalışanın envanterleri
        /// </summary>
        public List<InventoryQueueModel> Inventories { get; set; }
    }
}
