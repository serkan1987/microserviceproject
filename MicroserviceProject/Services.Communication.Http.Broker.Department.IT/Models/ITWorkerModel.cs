﻿using System;
using System.Collections.Generic;

namespace Services.Communication.Http.Broker.Department.IT.Models
{
    /// <summary>
    /// Çalışanlar
    /// </summary>
    public class ITWorkerModel
    {
        public int Id { get; set; }    

        /// <summary>
        /// Başlama tarihi
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Bitiş tarihi
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Çalışanın IT envanterleri
        /// </summary>
        public List<ITInventoryModel> Inventories { get; set; }
    }
}
