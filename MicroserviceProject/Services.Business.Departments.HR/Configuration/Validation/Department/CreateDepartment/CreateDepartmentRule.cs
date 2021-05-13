﻿using FluentValidation;

using Infrastructure.Communication.Model.Department.HR;

namespace Services.Business.Departments.HR.Configuration.Validation.Department.CreateDepartment
{
    /// <summary>
    /// Department/CreateDepartment Http endpoint için validasyon kuralı
    /// </summary>
    public class CreatePersonRule : AbstractValidator<DepartmentModel>
    {
        /// <summary>
        /// Department/CreateDepartment Http endpoint için validasyon kuralı
        /// </summary>
        public CreatePersonRule()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Departman adı boş geçilemez");
        }
    }
}
