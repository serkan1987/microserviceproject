﻿using FluentValidation;

using Services.Communication.Http.Broker.Department.Finance.Models;

namespace Services.Business.Departments.Finance.Configuration.Validation.Cost.DecideCost
{
    /// <summary>
    /// Cost/DecideCost Http endpoint için validasyon kuralı
    /// </summary>
    public class DecideCostRule : AbstractValidator<DecidedCostModel>
    {
        /// <summary>
        /// Cost/DecideCost Http endpoint için validasyon kuralı
        /// </summary>
        public DecideCostRule()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Masraf Id geçersiz");
        }
    }
}
