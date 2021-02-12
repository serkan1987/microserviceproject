﻿using FluentValidation;

using MicroserviceProject.Services.Business.Model.Department.HR;

using System.Linq;

namespace MicroserviceProject.Services.Business.Departments.AA.Configuration.Validation.Inventory.AssignInventoryToWorker
{
    /// <summary>
    /// Inventory/AssignInventoryToWorker Http endpoint için validasyon kuralı
    /// </summary>
    public class AssignInventoryToWorkerRule : AbstractValidator<WorkerModel>
    {
        /// <summary>
        /// Inventory/AssignInventoryToWorker Http endpoint için validasyon kuralı
        /// </summary>
        public AssignInventoryToWorkerRule()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Çalışan Id si boş geçilemez");
            RuleFor(x => x.AAInventories).NotNull().WithMessage("Envanter bilgisi boş geçilemez");
            RuleFor(x => x.AAInventories.Any()).NotEqual(false).WithMessage("Envanter bilgisi boş geçilemez");
        }
    }
}
