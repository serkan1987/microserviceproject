﻿namespace Services.Communication.Mq.Rabbit.Department.Models.IT
{
    /// <summary>
    /// Envanter talep modeli
    /// </summary>
    public class InventoryRequestQueueModel : BaseQueueModel
    {
        public int InventoryId { get; set; }
        public int Amount { get; set; }
        public bool Revoked { get; set; }
        public bool Done { get; set; }
    }
}
