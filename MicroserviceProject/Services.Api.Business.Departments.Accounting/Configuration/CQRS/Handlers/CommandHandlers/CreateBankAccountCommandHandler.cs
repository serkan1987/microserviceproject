﻿using MediatR;

using Services.Api.Business.Departments.Accounting.Services;
using Services.Api.Business.Departments.Accounting.Util.Validation.Department.CreateDepartment;
using Services.Communication.Http.Broker.Department.Accounting.CQRS.Commands.Requests;
using Services.Communication.Http.Broker.Department.Accounting.CQRS.Commands.Responses;
using Services.Logging.Aspect.Handlers;

using System.Threading;
using System.Threading.Tasks;

namespace Services.Api.Business.Departments.Accounting.Configuration.CQRS.Handlers.CommandHandlers
{
    public class CreateBankAccountCommandHandler : IRequestHandler<AccountingCreateBankAccountCommandRequest, AccountingCreateBankAccountCommandResponse>
    {
        private readonly RuntimeHandler _runtimeHandler;
        private readonly BankService _bankService;
        private readonly CreateBankAccountValidator _createBankAccountValidator;

        public CreateBankAccountCommandHandler(
            RuntimeHandler runtimeHandler,
            BankService bankService,
            CreateBankAccountValidator createBankAccountValidator)
        {
            _runtimeHandler = runtimeHandler;
            _bankService = bankService;
            _createBankAccountValidator = createBankAccountValidator;
        }

        public async Task<AccountingCreateBankAccountCommandResponse> Handle(AccountingCreateBankAccountCommandRequest request, CancellationToken cancellationToken)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            await _createBankAccountValidator.ValidateAsync(request.BankAccount, cancellationTokenSource);

            return new AccountingCreateBankAccountCommandResponse()
            {
                CreatedBankAccountId =
                await
                _runtimeHandler.ExecuteResultMethod<Task<int>>(
                    _bankService,
                    nameof(_bankService.CreateBankAccountAsync),
                    new object[] { request.BankAccount, cancellationTokenSource })
            };
        }
    }
}
