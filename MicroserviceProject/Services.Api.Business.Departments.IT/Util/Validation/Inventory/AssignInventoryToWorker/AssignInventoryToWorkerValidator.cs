﻿using Services.Communication.Http.Broker.Department.IT.Models;

using FluentValidation.Results;

using Infrastructure.Validation.Exceptions;
using Infrastructure.Validation.Models;

using Services.Api.Business.Departments.IT.Configuration.Validation.Inventory.AssignInventoryToWorker;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Api.Business.Departments.IT.Util.Validation.Inventory.AssignInventoryToWorker
{
    /// <summary>
    /// Inventory/AssignInventoryToWorker Http endpoint için validasyon kuralını doğrulayan sınıf
    /// </summary>
    public class AssignInventoryToWorkerValidator
    {
        /// <summary>
        /// Request body doğrular
        /// </summary>
        /// <param name="worker">Doğrulanacak nesne</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        public static async Task ValidateAsync(List<ITAssignInventoryToWorkerModel> workerModels, CancellationTokenSource cancellationTokenSource)
        {
            AssignInventoryToWorkerRule validationRules = new AssignInventoryToWorkerRule();

            if (workerModels != null)
            {
                foreach (var worker in workerModels)
                {
                    ValidationResult validationResult = await validationRules.ValidateAsync(worker, cancellationTokenSource.Token);

                    if (!validationResult.IsValid)
                    {
                        ValidationModel validation = new ValidationModel()
                        {
                            IsValid = false,
                            ValidationItems = new List<ValidationItemModel>()
                        };

                        validation.ValidationItems.AddRange(
                            validationResult.Errors.Select(x => new ValidationItemModel()
                            {
                                Key = x.PropertyName,
                                Value = x.AttemptedValue,
                                Message = x.ErrorMessage
                            }).ToList());

                        throw new ValidationException(validation);
                    }
                }
            }
            else
            {
                ValidationModel validation = new ValidationModel()
                {
                    IsValid = false,
                    ValidationItems = new List<ValidationItemModel>()
                };

                throw new ValidationException(validation);
            }
        }
    }
}
