﻿using FluentValidation.Results;

using MicroserviceProject.Infrastructure.Communication.Moderator.Exceptions;
using MicroserviceProject.Infrastructure.Communication.Moderator.Model.Basics;
using MicroserviceProject.Infrastructure.Communication.Moderator.Model.Errors;
using MicroserviceProject.Infrastructure.Communication.Moderator.Model.Validations;
using MicroserviceProject.Services.Business.Departments.AA.Configuration.Validation.Inventory.CreateInventory;
using MicroserviceProject.Services.Business.Departments.AA.Configuration.Validation.Transaction;
using MicroserviceProject.Services.Model.Department.AA;
using MicroserviceProject.Services.Transaction.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceProject.Services.Business.Departments.AA.Util.Validation.Transaction
{
    /// <summary>
    /// Transaction/RollbackTransaction Http endpoint için validasyon kuralını doğrulayan sınıf
    /// </summary>
    public class RollbackTransactionValidator
    {
        /// <summary>
        /// Request body doğrular
        /// </summary>
        /// <param name="rollbackModel">Doğrulanacak nesne</param>
        /// <param name="cancellationToken">İptal tokenı</param>
        /// <returns></returns>
        public static async Task ValidateAsync(RollbackModel rollbackModel, CancellationToken cancellationToken)
        {
            RollbackTransactionRule validationRules = new RollbackTransactionRule();

            if (rollbackModel != null)
            {
                ValidationResult validationResult = await validationRules.ValidateAsync(rollbackModel, cancellationToken);

                if (!validationResult.IsValid)
                {
                    ServiceResult serviceResult = new ServiceResult()
                    {
                        IsSuccess = false,
                        Error = new Error()
                        {
                            Description = "Geçersiz parametre"
                        },
                        Validation = new Infrastructure.Communication.Moderator.Model.Validations.Validation()
                        {
                            IsValid = false,
                            ValidationItems = new List<ValidationItem>()
                        }
                    };
                    serviceResult.Validation.ValidationItems.AddRange(
                        validationResult.Errors.Select(x => new ValidationItem()
                        {
                            Key = x.PropertyName,
                            Value = x.AttemptedValue,
                            Message = x.ErrorMessage
                        }).ToList());

                    throw new ValidationException(serviceResult);
                }
            }
            else
            {
                ServiceResult serviceResult = new ServiceResult()
                {
                    IsSuccess = false,
                    Error = new Error()
                    {
                        Description = "Geçersiz parametre"
                    },
                    Validation = new Infrastructure.Communication.Moderator.Model.Validations.Validation()
                    {
                        IsValid = false,
                        ValidationItems = new List<ValidationItem>()
                    }
                };

                throw new ValidationException(serviceResult);
            }
        }
    }
}