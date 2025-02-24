﻿using FluentValidation;

using Infrastructure.Transaction.Recovery;

namespace Services.Business.Departments.Finance.Configuration.Validation.Transaction
{
    /// <summary>
    /// Transaction/RollbackTransactionRule Http endpoint için validasyon kuralı
    /// </summary>
    public class RollbackTransactionRule : AbstractValidator<RollbackModel>
    {
        /// <summary>
        /// Transaction/RollbackTransactionRule Http endpoint için validasyon kuralı
        /// </summary>
        public RollbackTransactionRule()
        {
            RuleFor(x => x.TransactionIdentity).NotEmpty().WithMessage("İşlem kimliği boş geçilemez");
        }
    }
}
