﻿using Infrastructure.Communication.Model.Basics;
using Infrastructure.Communication.Model.Department.HR;
using Infrastructure.Communication.Moderator;
using Infrastructure.Routing.Providers;
using Infrastructure.Transaction.ExecutionHandler;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Gateway.Public.Services;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Gateway.Public.Controllers
{
    [Authorize]
    [Route("HR")]
    public class HumanResourcesController : Controller
    {
        private readonly HRService hrService;

        public HumanResourcesController(HRService hrService)
        {
            this.hrService = hrService;
        }

        [HttpGet]
        [Route(nameof(GetDepartments))]
        public async Task<IActionResult> GetDepartments(CancellationTokenSource cancellationTokenSource)
        {
            hrService.TransactionIdentity = Guid.NewGuid().ToString();

            return await ServiceExecuter.ExecuteServiceAsync(async () =>
            {
                return await hrService.GetDepartmentsAsync(cancellationTokenSource);
            }, hrService);
        }
    }
}
