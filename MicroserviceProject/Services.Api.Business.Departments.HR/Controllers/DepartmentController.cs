﻿using Infrastructure.Communication.Http.Wrapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Api.Business.Departments.HR.Services;
using Services.Communication.Http.Broker.Department.HR.CQRS.Commands.Requests;
using Services.Communication.Http.Broker.Department.HR.CQRS.Queries.Requests;
using Services.Communication.Http.Broker.Department.HR.CQRS.Queries.Responses;
using Services.Communication.Http.Broker.Department.HR.Models;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Api.Business.Departments.HR.Controllers
{
    [Route("Department")]
    public class DepartmentController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly DepartmentService _departmentService;

        public DepartmentController(IMediator mediator, DepartmentService departmentService)
        {
            _mediator = mediator;
            _departmentService = departmentService;
        }

        [HttpGet]
        [Route(nameof(GetDepartments))]
        [Authorize(Roles = "ApiUser,GatewayUser")]
        public async Task<IActionResult> GetDepartments()
        {
            return await HttpResponseWrapper.WrapAsync<List<DepartmentModel>>(async () =>
            {
                if (ByPassMediatR)
                    return await _departmentService.GetDepartmentsAsync(new CancellationTokenSource());
                else
                {
                    GetDepartmentsQueryResponse mediatorResult = await _mediator.Send(new GetDepartmentsQueryRequest());

                    return mediatorResult.Departments;
                }
            },
            services: _departmentService);
        }

        [HttpPost]
        [Route(nameof(CreateDepartment))]
        [Authorize(Roles = "ApiUser,GatewayUser,QueueUser")]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentCommandRequest request)
        {
            return await HttpResponseWrapper.WrapAsync(async () =>
            {
                if (ByPassMediatR)
                    await _departmentService.CreateDepartmentAsync(request.Department, new CancellationTokenSource());
                else
                    await _mediator.Send(request);
            },
            services: _departmentService);
        }
    }
}
