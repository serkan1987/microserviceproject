﻿using MediatR;

using Services.Business.Departments.Finance.Services;
using Services.Business.Departments.Finance.Util.Validation.Request.CreateProductionRequest;
using Services.Communication.Http.Broker.Department.Finance.CQRS.Commands.Requests;
using Services.Communication.Http.Broker.Department.Finance.CQRS.Commands.Responses;
using Services.Logging.Aspect.Handlers;

using System.Threading;
using System.Threading.Tasks;

namespace Services.Api.Business.Departments.Finance.Configuration.CQRS.Handlers.CommandHandlers
{
    public class CreateProductionRequestCommandHandler : IRequestHandler<CreateProductionRequestCommandRequest, CreateProductionRequestCommandResponse>
    {
        private readonly RuntimeHandler _runtimeHandler;
        private readonly ProductionRequestService _productionRequestService;
        private readonly CreateProductionRequestValidator _createProductionRequestValidator;

        public CreateProductionRequestCommandHandler(
            RuntimeHandler runtimeHandler,
            ProductionRequestService productionRequestService,
            CreateProductionRequestValidator createProductionRequestValidator)
        {
            _runtimeHandler = runtimeHandler;
            _productionRequestService = productionRequestService;
            _createProductionRequestValidator = createProductionRequestValidator;
        }

        public async Task<CreateProductionRequestCommandResponse> Handle(CreateProductionRequestCommandRequest request, CancellationToken cancellationToken)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            await _createProductionRequestValidator.ValidateAsync(request.ProductionRequest, cancellationTokenSource);

            return new CreateProductionRequestCommandResponse()
            {
                CreatedProductionRequest =
                await
                _runtimeHandler.ExecuteResultMethod<Task<int>>(
                    _productionRequestService,
                    nameof(_productionRequestService.CreateProductionRequestAsync),
                    new object[] { request.ProductionRequest, cancellationTokenSource })
            };
        }
    }
}
