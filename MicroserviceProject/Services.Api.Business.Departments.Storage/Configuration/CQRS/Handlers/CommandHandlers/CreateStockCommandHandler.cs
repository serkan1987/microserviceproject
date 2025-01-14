﻿using MediatR;

using Services.Api.Business.Departments.Storage.Services;
using Services.Api.Business.Departments.Storage.Util.Validation.Stock;
using Services.Communication.Http.Broker.Department.Storage.CQRS.Commands.Requests;
using Services.Communication.Http.Broker.Department.Storage.CQRS.Commands.Responses;
using Services.Logging.Aspect.Handlers;

using System.Threading;
using System.Threading.Tasks;

namespace Services.Api.Business.Departments.Storage.Configuration.CQRS.Handlers.CommandHandlers
{
    public class CreateStockCommandHandler : IRequestHandler<CreateStockCommandRequest, CreateStockCommandResponse>
    {
        private readonly RuntimeHandler _runtimeHandler;
        private readonly StockService _stockService;
        private readonly CreateStockValidator _createStockValidator;

        public CreateStockCommandHandler(
            RuntimeHandler runtimeHandler,
            StockService stockService,
            CreateStockValidator createStockValidator)
        {
            _runtimeHandler = runtimeHandler;
            _stockService = stockService;
            _createStockValidator = createStockValidator;
        }

        public async Task<CreateStockCommandResponse> Handle(CreateStockCommandRequest request, CancellationToken cancellationToken)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            await _createStockValidator.ValidateAsync(request.Stock, cancellationTokenSource);

            return new CreateStockCommandResponse()
            {
                CreatedStockId =
                await _runtimeHandler.ExecuteResultMethod<Task<int>>(
                    _stockService,
                    nameof(_stockService.CreateStockAsync),
                    new object[] { request.Stock, cancellationTokenSource })
            };
        }
    }
}
