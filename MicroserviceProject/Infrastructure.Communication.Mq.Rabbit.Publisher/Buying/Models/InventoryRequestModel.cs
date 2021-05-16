﻿namespace Infrastructure.Communication.Mq.Rabbit.Publisher.Buying.Models
{
    /// <summary>
    /// Envanter satın alım talebi modeli
    /// </summary>
    public class InventoryRequestModel
    {
        public int InventoryId { get; set; }
        public int DepartmentId { get; set; }
        public int Amount { get; set; }
    }
}
