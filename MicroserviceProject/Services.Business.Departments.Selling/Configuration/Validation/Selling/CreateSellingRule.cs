﻿using FluentValidation;

using Services.Business.Departments.Selling.Models;

namespace Services.Business.Departments.Selling.Configuration.Validation.Selling
{
    /// <summary>
    /// Selling/CreateSelling Http endpoint için validasyon kuralı
    /// </summary>
    public class CreateSellingRule : AbstractValidator<SellModel>
    {
        /// <summary>
        /// Selling/CreateSelling Http endpoint için validasyon kuralı
        /// </summary>
        public CreateSellingRule()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage("Müşteri Id boş geçilemez");
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Ürün Id boş geçilemez");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Miktar sıfırdan büyük olmalı");
        }
    }
}