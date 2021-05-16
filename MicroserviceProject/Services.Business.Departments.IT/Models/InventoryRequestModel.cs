﻿namespace Services.Business.Departments.IT.Models
{
    /// <summary>
    /// Envanter talep modeli
    /// </summary>
    public class InventoryRequestModel
    {
        public int InventoryId { get; set; }
        public int DepartmentId { get; set; }
        public int Amount { get; set; }
        public bool Revoked { get; set; }
    }
}
