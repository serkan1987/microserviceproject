﻿using MicroserviceProject.Services.Business.Departments.Accounting.Services;
using MicroserviceProject.Services.Business.Departments.Accounting.Util.Validation.Department.CreateDepartment;
using MicroserviceProject.Services.Model.Department.Accounting;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceProject.Services.Business.Departments.Accounting.Controllers
{
    [Authorize]
    [Route("BankAccounts")]
    public class AccountController : Controller
    {
        private readonly BankService _bankService;

        public AccountController(BankService bankService)
        {
            _bankService = bankService;
        }

        [HttpGet]
        [Route(nameof(GetBankAccountsOfWorker))]
        public async Task<IActionResult> GetBankAccountsOfWorker(int workerId, CancellationTokenSource cancellationTokenSource)
        {
            if (Request != null && Request.Headers.ContainsKey("TransactionIdentity"))
            {
                _bankService.TransactionIdentity = Request.Headers["TransactionIdentity"].ToString();
            }

            return await ServiceExecuter.ExecuteServiceAsync<List<BankAccountModel>>(async () =>
            {
                return await _bankService.GetBankAccounts(workerId, cancellationTokenSource);
            },
            services: _bankService);
        }

        [HttpPost]
        [Route(nameof(CreateBankAccount))]
        public async Task<IActionResult> CreateBankAccount([FromBody] BankAccountModel bankAccount, CancellationTokenSource cancellationTokenSource)
        {
            if (Request != null && Request.Headers.ContainsKey("TransactionIdentity"))
            {
                _bankService.TransactionIdentity = Request.Headers["TransactionIdentity"].ToString();
            }

            return await ServiceExecuter.ExecuteServiceAsync<int>(async () =>
            {
                await CreateBankAccountValidator.ValidateAsync(bankAccount, cancellationTokenSource);

                return await _bankService.CreateBankAccountAsync(bankAccount, cancellationTokenSource);
            },
            services: _bankService);
        }

        [HttpGet]
        [Route(nameof(GetCurrencies))]
        public async Task<IActionResult> GetCurrencies(CancellationTokenSource cancellationTokenSource)
        {
            if (Request != null && Request.Headers.ContainsKey("TransactionIdentity"))
            {
                _bankService.TransactionIdentity = Request.Headers["TransactionIdentity"].ToString();
            }

            return await ServiceExecuter.ExecuteServiceAsync<List<CurrencyModel>>(async () =>
            {
                return await _bankService.GetCurrenciesAsync(cancellationTokenSource);
            },
            services: _bankService);
        }

        [HttpPost]
        [Route(nameof(CreateCurrency))]
        public async Task<IActionResult> CreateCurrency([FromBody] CurrencyModel currency, CancellationTokenSource cancellationTokenSource)
        {
            if (Request != null && Request.Headers.ContainsKey("TransactionIdentity"))
            {
                _bankService.TransactionIdentity = Request.Headers["TransactionIdentity"].ToString();
            }

            return await ServiceExecuter.ExecuteServiceAsync<int>(async () =>
            {
                await CreateCurrencyValidator.ValidateAsync(currency, cancellationTokenSource);

                return await _bankService.CreateCurrencyAsync(currency, cancellationTokenSource);
            },
            services: _bankService);
        }

        [HttpGet]
        [Route(nameof(GetSalaryPaymentsOfWorker))]
        public async Task<IActionResult> GetSalaryPaymentsOfWorker(int workerId, CancellationTokenSource cancellationTokenSource)
        {
            if (Request != null && Request.Headers.ContainsKey("TransactionIdentity"))
            {
                _bankService.TransactionIdentity = Request.Headers["TransactionIdentity"].ToString();
            }

            return await ServiceExecuter.ExecuteServiceAsync<List<SalaryPaymentModel>>(async () =>
            {
                return await _bankService.GetSalaryPaymentsOfWorkerAsync(workerId, cancellationTokenSource);
            },
            services: _bankService);
        }

        [HttpPost]
        [Route(nameof(CreateSalaryPayment))]
        public async Task<IActionResult> CreateSalaryPayment([FromBody] SalaryPaymentModel salaryPayment, CancellationTokenSource cancellationTokenSource)
        {
            if (Request != null && Request.Headers.ContainsKey("TransactionIdentity"))
            {
                _bankService.TransactionIdentity = Request.Headers["TransactionIdentity"].ToString();
            }

            return await ServiceExecuter.ExecuteServiceAsync<int>(async () =>
            {
                await CreateSalaryPaymentValidator.ValidateAsync(salaryPayment, cancellationTokenSource);

                return await _bankService.CreateSalaryPaymentAsync(salaryPayment, cancellationTokenSource);
            },
            services: _bankService);
        }
    }
}