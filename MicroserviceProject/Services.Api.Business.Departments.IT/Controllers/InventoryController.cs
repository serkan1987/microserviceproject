﻿using Infrastructure.Communication.Http.Wrapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Api.Business.Departments.IT.Services;
using Services.Communication.Http.Broker.Department.IT.CQRS.Commands.Requests;
using Services.Communication.Http.Broker.Department.IT.CQRS.Queries.Requests;
using Services.Communication.Http.Broker.Department.IT.CQRS.Queries.Responses;

using System.Threading.Tasks;

namespace Services.Api.Business.Departments.IT.Controllers
{
    [Route("Inventory")]
    public class InventoryController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly InventoryService _inventoryService;

        public InventoryController(IMediator mediator, InventoryService inventoryService)
        {
            _mediator = mediator;
            _inventoryService = inventoryService;
        }

        [HttpGet]
        [Route(nameof(GetInventories))]
        [Authorize(Roles = "ApiUser,GatewayUser")]
        public async Task<IActionResult> GetInventories()
        {
            return await HttpResponseWrapper.WrapAsync(async () =>
            {
                ITGetInventoriesQueryResponse mediatorResult = await _mediator.Send(new ITGetInventoriesQueryRequest());

                return mediatorResult.Inventories;
            },
            services: _inventoryService);
        }

        [HttpPost]
        [Route(nameof(CreateInventory))]
        [Authorize(Roles = "ApiUser,GatewayUser,QueueUser")]
        public async Task<IActionResult> CreateInventory([FromBody] ITCreateInventoryCommandRequest request)
        {
            return await HttpResponseWrapper.WrapAsync(async () =>
            {
                await _mediator.Send(request);
            },
            services: _inventoryService);
        }

        [HttpPost]
        [Route(nameof(AssignInventoryToWorker))]
        [Authorize(Roles = "ApiUser,GatewayUser,QueueUser")]
        public async Task<IActionResult> AssignInventoryToWorker([FromBody] ITAssignInventoryToWorkerCommandRequest request)
        {
            return await HttpResponseWrapper.WrapAsync(async () =>
            {
                await _mediator.Send(request);
            },
            services: _inventoryService);
        }

        [HttpPost]
        [Route(nameof(CreateDefaultInventoryForNewWorker))]
        [Authorize(Roles = "ApiUser,GatewayUser,QueueUser")]
        public async Task<IActionResult> CreateDefaultInventoryForNewWorker([FromBody] ITCreateDefaultInventoryForNewWorkerCommandRequest request)
        {
            return await HttpResponseWrapper.WrapAsync(async () =>
            {
                await _mediator.Send(request);
            },
            services: _inventoryService);
        }

        [HttpGet]
        [Route(nameof(GetInventoriesForNewWorker))]
        [Authorize(Roles = "ApiUser,GatewayUser,QueueUser")]
        public IActionResult GetInventoriesForNewWorker()
        {
            return HttpResponseWrapper.Wrap(() =>
            {
                ITGetInventoriesForNewWorkerQueryResponse mediatorResult = _mediator.Send(new ITGetInventoriesForNewWorkerQueryRequest()).Result;

                return mediatorResult.Inventories;
            },
            services: _inventoryService);
        }

        [HttpPost]
        [Route(nameof(InformInventoryRequest))]
        [Authorize(Roles = "ApiUser,QueueUser")]
        public async Task<IActionResult> InformInventoryRequest([FromBody] ITInformInventoryRequestCommandRequest request)
        {
            return await HttpResponseWrapper.WrapAsync(async () =>
            {
                await _mediator.Send(request);
            },
            services: _inventoryService);
        }
    }
}
