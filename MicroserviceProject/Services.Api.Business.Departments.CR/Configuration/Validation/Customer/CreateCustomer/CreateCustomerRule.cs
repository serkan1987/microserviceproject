﻿using Services.Communication.Http.Broker.Department.CR.Models;

using FluentValidation;

namespace Services.Api.Business.Departments.CR.Configuration.Validation.Customer.CreateCustomer
{
    /// <summary>
    /// Customer/CreateCustomer Http endpoint için validasyon kuralı
    /// </summary>
    public class CreateCustomerRule : AbstractValidator<CustomerModel>
    {
        /// <summary>
        /// Customer/CreateCustomer Http endpoint için validasyon kuralı
        /// </summary>
        public CreateCustomerRule()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Müşteri adı boş geçilemez");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Müşteri soyadı boş geçilemez");
        }
    }
}
