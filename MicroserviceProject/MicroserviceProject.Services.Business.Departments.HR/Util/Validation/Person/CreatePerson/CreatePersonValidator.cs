﻿using FluentValidation.Results;

using MicroserviceProject.Infrastructure.Validation.Exceptions;
using MicroserviceProject.Infrastructure.Validation.Model;
using MicroserviceProject.Services.Business.Departments.HR.Configuration.Validation.Person.CreatePerson;
using MicroserviceProject.Services.Model.Department.HR;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceProject.Services.Business.Departments.HR.Util.Validation.Person.CreatePerson
{
    /// <summary>
    /// Person/CreatePerson Http endpoint için validasyon kuralını doğrulayan sınıf
    /// </summary>
    public class CreatePersonValidator
    {
        /// <summary>
        /// Request body doğrular
        /// </summary>
        /// <param name="person">Doğrulanacak nesne</param>
        /// <param name="cancellationToken">İptal tokenı</param>
        /// <returns></returns>
        public static async Task ValidateAsync(PersonModel person, CancellationToken cancellationToken)
        {
            CreatePersonRule validationRules = new CreatePersonRule();

            if (person != null)
            {
                ValidationResult validationResult = await validationRules.ValidateAsync(person, cancellationToken);

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
