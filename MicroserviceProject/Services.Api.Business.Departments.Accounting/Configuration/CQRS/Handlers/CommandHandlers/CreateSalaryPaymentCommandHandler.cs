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
    public class CreateSalaryPaymentCommandHandler : IRequestHandler<AccountingCreateSalaryPaymentCommandRequest, AccountingCreateSalaryPaymentCommandResponse>
    {
        private readonly RuntimeHandler _runtimeHandler;
        private readonly BankService _bankService;
        private readonly CreateSalaryPaymentValidator _createSalaryPaymentValidator;

        public CreateSalaryPaymentCommandHandler(
            RuntimeHandler runtimeHandler,
            BankService bankService,
            CreateSalaryPaymentValidator createSalaryPaymentValidator)
        {
            _runtimeHandler = runtimeHandler;
            _bankService = bankService;
            _createSalaryPaymentValidator = createSalaryPaymentValidator;
        }

        public async Task<AccountingCreateSalaryPaymentCommandResponse> Handle(AccountingCreateSalaryPaymentCommandRequest request, CancellationToken cancellationToken)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            await _createSalaryPaymentValidator.ValidateAsync(request.SalaryPayment, cancellationTokenSource);

            return new AccountingCreateSalaryPaymentCommandResponse()
            {
                CreatedSalaryPaymentId =
                await
                _runtimeHandler.ExecuteResultMethod<Task<int>>(
                    _bankService,
                    nameof(_bankService.CreateSalaryPaymentAsync),
                    new object[] { request.SalaryPayment, cancellationTokenSource })
            };
        }
    }
}
