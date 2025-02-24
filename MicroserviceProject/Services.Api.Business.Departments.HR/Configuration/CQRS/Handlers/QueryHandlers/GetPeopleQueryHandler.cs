﻿using MediatR;

using Services.Api.Business.Departments.HR.Services;
using Services.Communication.Http.Broker.Department.HR.CQRS.Queries.Requests;
using Services.Communication.Http.Broker.Department.HR.CQRS.Queries.Responses;
using Services.Communication.Http.Broker.Department.HR.Models;
using Services.Logging.Aspect.Handlers;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Api.Business.Departments.HR.Configuration.CQRS.Handlers.QueryHandlers
{
    public class GetPeopleQueryHandler : IRequestHandler<GetPeopleQueryRequest, GetPeopleQueryResponse>
    {
        private readonly RuntimeHandler _runtimeHandler;
        private readonly PersonService _personService;

        public GetPeopleQueryHandler(
            RuntimeHandler runtimeHandler,
            PersonService personService)
        {
            _runtimeHandler = runtimeHandler;
            _personService = personService;
        }

        public async Task<GetPeopleQueryResponse> Handle(GetPeopleQueryRequest request, CancellationToken cancellationToken)
        {
            return new GetPeopleQueryResponse()
            {
                People =
                await
                _runtimeHandler.ExecuteResultMethod<Task<List<PersonModel>>>(
                    _personService,
                    nameof(_personService.GetPeopleAsync),
                    new object[] { new CancellationTokenSource() })
            };
        }
    }
}
