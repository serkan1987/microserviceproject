﻿using System;

namespace Services.Communication.Http.Broker.Department.AA.Models
{
    /// <summary>
    /// Envanter modeli
    /// </summary>
    public class InventoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CurrentStockCount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}