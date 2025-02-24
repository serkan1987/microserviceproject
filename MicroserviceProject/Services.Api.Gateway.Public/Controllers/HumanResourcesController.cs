﻿using Infrastructure.Communication.Http.Wrapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Api.Gateway.Util.Communication;
using Services.Communication.Http.Broker.Department.HR.Abstract;

using System.Threading;
using System.Threading.Tasks;

namespace Services.Api.Gateway.Public.Controllers
{
    [Route("HR")]
    public class HumanResourcesController : Controller
    {
        private readonly ApiBridge _apiBridge;
        private readonly IHRCommunicator _hrCommunicator;

        public HumanResourcesController(
            ApiBridge apiBridge,
            IHRCommunicator hrCommunicator)
        {
            _apiBridge = apiBridge;
            _hrCommunicator = hrCommunicator;
        }

        [HttpGet]
        [Route(nameof(GetDepartments))]
        [Authorize(Roles = "WebPresentationUser")]
        //[EnableRateLimiting("DefaultFixedLimiterPolicy")]
        public async Task<IActionResult> GetDepartments(CancellationTokenSource cancellationTokenSource)
        {
            return await HttpResponseWrapper.WrapAsync(async () =>
            {
                return await _apiBridge.CallAsync(async (transactionIdentity, cancellationTokenSource) =>
                {
                    return await _hrCommunicator.GetDepartmentsAsync(transactionIdentity, cancellationTokenSource);

                }, cancellationTokenSource);
            });
        }
    }
}
