﻿using MediatR;

using Services.Api.Business.Departments.CR.Services;
using Services.Communication.Http.Broker.Department.CR.CQRS.Queries.Requests;
using Services.Communication.Http.Broker.Department.CR.CQRS.Queries.Responses;
using Services.Communication.Http.Broker.Department.CR.Models;
using Services.Logging.Aspect.Handlers;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Api.Business.Departments.CR.Configuration.CQRS.Handlers.QueryHandlers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQueryRequest, GetCustomersQueryResponse>
    {
        private readonly RuntimeHandler _runtimeHandler;
        private readonly CustomerService _customerService;

        public GetCustomersQueryHandler(
            RuntimeHandler runtimeHandler,
            CustomerService customerService)
        {
            _runtimeHandler = runtimeHandler;
            _customerService = customerService;
        }

        public async Task<GetCustomersQueryResponse> Handle(GetCustomersQueryRequest request, CancellationToken cancellationToken)
        {
            return new GetCustomersQueryResponse()
            {
                Customers =
                await
                _runtimeHandler.ExecuteResultMethod<Task<List<CustomerModel>>>(
                    _customerService,
                    nameof(_customerService.GetCustomersAsync),
                    new object[] { new CancellationTokenSource() })
            };
        }
    }
}
