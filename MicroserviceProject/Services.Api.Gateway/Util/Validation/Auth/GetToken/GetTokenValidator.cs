﻿using FluentValidation.Results;

using Infrastructure.Communication.Http.Models;
using Infrastructure.Security.Model;
using Infrastructure.Validation.Models;

using Services.Api.Gateway.Validation.Auth.GetToken;

namespace Services.Api.Gateway.Util.Validation.Auth.GetToken
{
    /// <summary>
    /// Auth/GetToken endpoint için validasyon
    /// </summary>
    public class GetTokenValidator
    {
        /// <summary>
        /// Request body doğrular
        /// </summary>
        /// <param name="credential">Doğrulanacak nesne</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        public static async Task<ServiceResultModel> ValidateAsync(AuthenticationCredential credential, CancellationTokenSource cancellationTokenSource)
        {
            CredentialRule validationRules = new CredentialRule();

            if (credential != null)
            {
                ValidationResult validationResult = await validationRules.ValidateAsync(credential, cancellationTokenSource.Token);

                if (!validationResult.IsValid)
                {
                    ServiceResultModel serviceResult = new ServiceResultModel()
                    {
                        IsSuccess = false,
                        ErrorModel = new ErrorModel()
                        {
                            Description = "Geçersiz parametre"
                        },
                        Validation = new ValidationModel()
                        {
                            IsValid = false,
                            ValidationItems = new List<ValidationItemModel>()
                        }
                    };
                    serviceResult.Validation.ValidationItems.AddRange(
                        validationResult.Errors.Select(x => new ValidationItemModel()
                        {
                            Key = x.PropertyName,
                            Value = x.AttemptedValue,
                            Message = x.ErrorMessage
                        }).ToList());

                    return serviceResult;
                }

                return new ServiceResultModel() { IsSuccess = true };
            }
            else
            {
                ServiceResultModel serviceResult = new ServiceResultModel()
                {
                    IsSuccess = false,
                    ErrorModel = new ErrorModel()
                    {
                        Description = "Geçersiz parametre"
                    },
                    Validation = new ValidationModel()
                    {
                        IsValid = false,
                        ValidationItems = new List<ValidationItemModel>()
                    }
                };

                return serviceResult;
            }
        }
    }
}
