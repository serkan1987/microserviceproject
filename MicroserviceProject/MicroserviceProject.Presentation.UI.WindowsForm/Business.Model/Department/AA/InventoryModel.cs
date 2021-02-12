﻿using System;

namespace MicroserviceProject.Presentation.UI.Business.Model.Department.AA
{
    /// <summary>
    /// İdari işler envanterleri
    /// </summary>
    public class InventoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
