﻿
using Infrastructure.Communication.Http.Models;

using Microsoft.AspNetCore.Mvc;

using Services.Communication.Http.Broker.Authorization.Models;
using Services.Api.Authorization.Business.Services;
using Services.Api.Authorization.Persistence.Sql.Exceptions;
using Services.Api.Authorization.Util.Validation.Auth.GetToken;

using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Validation.Exceptions;

namespace Services.Api.Authorization.Controllers
{
    [Route("Auth")]
    public class AuthController : Controller
    {
        private readonly SessionService _sessionService;
        private readonly UserService _userService;
        private readonly GetTokenValidator _getTokenValidator;

        public AuthController(
            SessionService sessionService,
            UserService userService,
            GetTokenValidator getTokenValidator)
        {
            _sessionService = sessionService;
            _userService = userService;
            _getTokenValidator = getTokenValidator;
        }

        [Route("GetToken")]
        [HttpPost]
        public async Task<IActionResult> GetToken([FromBody] CredentialModel credential, CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                await _getTokenValidator.ValidateAsync(credential, cancellationTokenSource);

                var token = await _sessionService.GetTokenAsync(credential, new CancellationTokenSource());

                return Ok(new ServiceResultModel<TokenModel>()
                {
                    IsSuccess = true,
                    Data = token
                });
            }
            catch (ValidationException vex)
            {
                return BadRequest(vex.ValidationResult);
            }
            catch (UserNotFoundException unf)
            {
                return Unauthorized(new ServiceResultModel()
                {
                    IsSuccess = false,
                    ErrorModel = new ErrorModel() { Description = unf.ToString() }
                });
            }
            catch (UndefinedGrantTypeException ugtex)
            {
                return Unauthorized(new ServiceResultModel()
                {
                    IsSuccess = false,
                    ErrorModel = new ErrorModel() { Description = ugtex.ToString() }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResultModel()
                {
                    IsSuccess = false,
                    ErrorModel = new ErrorModel() { Description = ex.ToString() }
                });
            }
        }

        [Route("GetUser")]
        [HttpGet]
        public async Task<IActionResult> GetUser(string token)
        {
            try
            {
                UserModel user = await _userService.GetUserAsync(token, new CancellationTokenSource());

                return Ok(new ServiceResultModel<UserModel>()
                {
                    Data = user
                });
            }
            catch (UserNotFoundException unf)
            {
                return Unauthorized(new ServiceResultModel()
                {
                    IsSuccess = false,
                    ErrorModel = new ErrorModel() { Description = unf.ToString() }
                });
            }
            catch (UndefinedGrantTypeException ugtex)
            {
                return Unauthorized(new ServiceResultModel()
                {
                    IsSuccess = false,
                    ErrorModel = new ErrorModel() { Description = ugtex.ToString() }
                });
            }
            catch (SessionNotFoundException snf)
            {
                return Unauthorized(new ServiceResultModel()
                {
                    IsSuccess = false,
                    ErrorModel = new ErrorModel() { Description = snf.ToString() }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResultModel() { IsSuccess = false, ErrorModel = new ErrorModel() { Description = ex.ToString() } });
            }
        }

        [Route("CheckUser")]
        [HttpGet]
        public async Task<IActionResult> CheckUser(string email)
        {
            try
            {
                return Ok(new ServiceResultModel<bool>()
                {
                    Data = await _userService.CheckUserAsync(email, new CancellationTokenSource())
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new ServiceResultModel() { IsSuccess = false, ErrorModel = new ErrorModel() { Description = ex.ToString() } });
            }
        }

        [Route("RegisterUser")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] CredentialModel credential)
        {
            try
            {
                await _userService.RegisterUserAsync(credential, new CancellationTokenSource());

                return Ok(new ServiceResultModel()
                {
                    IsSuccess = true
                });
            }
            catch (UserNotFoundException unf)
            {
                return BadRequest(new ServiceResultModel() { IsSuccess = false, ErrorModel = new ErrorModel() { Description = unf.ToString() } });
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResultModel() { IsSuccess = false, ErrorModel = new ErrorModel() { Description = ex.ToString() } });
            }
        }
    }
}
