﻿using Infrastructure.Communication.Http.Endpoint.Abstract;
using Infrastructure.Communication.Http.Endpoint.Authentication;
using Infrastructure.Communication.Http.Endpoint.Util;
using Infrastructure.Communication.Http.Models;
using Infrastructure.ServiceDiscovery.Discoverer.Abstract;
using Infrastructure.ServiceDiscovery.Discoverer.Exceptions;
using Infrastructure.ServiceDiscovery.Discoverer.Models;

using Services.Communication.Http.Broker.Department.Abstract;
using Services.Communication.Http.Broker.Department.Accounting.Abstract;
using Services.Communication.Http.Broker.Department.Accounting.CQRS.Commands.Requests;
using Services.Communication.Http.Broker.Department.Accounting.Models;
using Services.Communication.Http.Endpoint.Department.Accounting;

namespace Services.Communication.Http.Broker.Department.Accounting
{
    public class AccountingCommunicator : IAccountingCommunicator
    {
        /// <summary>
        /// Kaynakların serbest bırakılıp bırakılmadığı bilgisi
        /// </summary>
        private bool disposed = false;

        private readonly IDepartmentCommunicator _departmentCommunicator;
        private readonly IServiceDiscoverer _serviceDiscoverer;

        public AccountingCommunicator(
            IDepartmentCommunicator departmentCommunicator,
            IServiceDiscoverer serviceDiscoverer)
        {
            _departmentCommunicator = departmentCommunicator;
            _serviceDiscoverer = serviceDiscoverer;
        }

        public async Task<ServiceResultModel<List<AccountingBankAccountModel>>> GetBankAccountsOfWorkerAsync(
            int workerId,
            string transactionIdentity,
            CancellationTokenSource cancellationTokenSource)
        {
            CachedServiceModel service = await _serviceDiscoverer.GetServiceAsync("Services.Api.Business.Departments.Accounting", cancellationTokenSource);

            IEndpoint endpoint = service.GetEndpoint(GetBankAccountsOfWorkerEndpoint.Path);

            if (endpoint != null)
            {
                string token = await _departmentCommunicator.GetServiceToken(cancellationTokenSource);

                IAuthenticatedEndpoint authenticatedEndpoint = endpoint.ConvertToAuthenticatedEndpoint(new TokenAuthentication(token));
                authenticatedEndpoint.Headers.Add(new HttpHeaderModel() { Name = "TransactionIdentity", Value = transactionIdentity });
                authenticatedEndpoint.Queries.Add(new HttpQueryModel() { Name = "workerId", Value = workerId.ToString() });

                return await _departmentCommunicator.CallAsync<List<AccountingBankAccountModel>>(authenticatedEndpoint, cancellationTokenSource);
            }
            else
                throw new EndpointNotFoundException();
        }

        public async Task<ServiceResultModel> CreateBankAccountAsync(
            AccountingCreateBankAccountCommandRequest request,
            string transactionIdentity,
            CancellationTokenSource cancellationTokenSource)
        {
            CachedServiceModel service = await _serviceDiscoverer.GetServiceAsync("Services.Api.Business.Departments.Accounting", cancellationTokenSource);

            IEndpoint endpoint = service.GetEndpoint(CreateBankAccountEndpoint.Path);

            if (endpoint != null)
            {
                string token = await _departmentCommunicator.GetServiceToken(cancellationTokenSource);

                IAuthenticatedEndpoint authenticatedEndpoint = endpoint.ConvertToAuthenticatedEndpoint(new TokenAuthentication(token));
                authenticatedEndpoint.Headers.Add(new HttpHeaderModel() { Name = "TransactionIdentity", Value = transactionIdentity });

                return await _departmentCommunicator.CallAsync<AccountingCreateBankAccountCommandRequest, Object>(authenticatedEndpoint, request, cancellationTokenSource);
            }
            else
                throw new EndpointNotFoundException();
        }

        public async Task<ServiceResultModel<List<AccountingCurrencyModel>>> GetCurrenciesAsync(
            string transactionIdentity,
            CancellationTokenSource cancellationTokenSource)
        {
            CachedServiceModel service = await _serviceDiscoverer.GetServiceAsync("Services.Api.Business.Departments.Accounting", cancellationTokenSource);

            IEndpoint endpoint = service.GetEndpoint(GetCurrenciesEndpoint.Path);

            if (endpoint != null)
            {
                string token = await _departmentCommunicator.GetServiceToken(cancellationTokenSource);

                IAuthenticatedEndpoint authenticatedEndpoint = endpoint.ConvertToAuthenticatedEndpoint(new TokenAuthentication(token));
                authenticatedEndpoint.Headers.Add(new HttpHeaderModel() { Name = "TransactionIdentity", Value = transactionIdentity });

                return await _departmentCommunicator.CallAsync<List<AccountingCurrencyModel>>(authenticatedEndpoint, cancellationTokenSource);
            }
            else
                throw new EndpointNotFoundException();
        }

        public async Task<ServiceResultModel> CreateCurrencyAsync(
            AccountingCreateCurrencyCommandRequest request,
            string transactionIdentity,
            CancellationTokenSource cancellationTokenSource)
        {
            CachedServiceModel service = await _serviceDiscoverer.GetServiceAsync("Services.Api.Business.Departments.Accounting", cancellationTokenSource);

            IEndpoint endpoint = service.GetEndpoint(CreateCurrencyEndpoint.Path);

            if (endpoint != null)
            {
                string token = await _departmentCommunicator.GetServiceToken(cancellationTokenSource);

                IAuthenticatedEndpoint authenticatedEndpoint = endpoint.ConvertToAuthenticatedEndpoint(new TokenAuthentication(token));
                authenticatedEndpoint.Headers.Add(new HttpHeaderModel() { Name = "TransactionIdentity", Value = transactionIdentity });

                return await _departmentCommunicator.CallAsync<AccountingCreateCurrencyCommandRequest, Object>(authenticatedEndpoint, request, cancellationTokenSource);
            }
            else
                throw new EndpointNotFoundException();
        }

        public async Task<ServiceResultModel<List<AccountingSalaryPaymentModel>>> GetSalaryPaymentsOfWorkerAsync(
            int workerId,
            string transactionIdentity,
            CancellationTokenSource cancellationTokenSource)
        {
            CachedServiceModel service = await _serviceDiscoverer.GetServiceAsync("Services.Api.Business.Departments.Accounting", cancellationTokenSource);

            IEndpoint endpoint = service.GetEndpoint(GetSalaryPaymentsOfWorkerEndpoint.Path);

            if (endpoint != null)
            {
                string token = await _departmentCommunicator.GetServiceToken(cancellationTokenSource);

                IAuthenticatedEndpoint authenticatedEndpoint = endpoint.ConvertToAuthenticatedEndpoint(new TokenAuthentication(token));
                authenticatedEndpoint.Headers.Add(new HttpHeaderModel() { Name = "TransactionIdentity", Value = transactionIdentity });
                authenticatedEndpoint.Queries.Add(new HttpQueryModel() { Name = "workerId", Value = workerId.ToString() });

                return await _departmentCommunicator.CallAsync<List<AccountingSalaryPaymentModel>>(authenticatedEndpoint, cancellationTokenSource);
            }
            else
                throw new EndpointNotFoundException();
        }

        public async Task<ServiceResultModel> CreateSalaryPaymentAsync(
           AccountingCreateSalaryPaymentCommandRequest request,
           string transactionIdentity,
           CancellationTokenSource cancellationTokenSource)
        {
            CachedServiceModel service = await _serviceDiscoverer.GetServiceAsync("Services.Api.Business.Departments.Accounting", cancellationTokenSource);

            IEndpoint endpoint = service.GetEndpoint(CreateSalaryPaymentEndpoint.Path);

            if (endpoint != null)
            {
                string token = await _departmentCommunicator.GetServiceToken(cancellationTokenSource);

                IAuthenticatedEndpoint authenticatedEndpoint = endpoint.ConvertToAuthenticatedEndpoint(new TokenAuthentication(token));
                authenticatedEndpoint.Headers.Add(new HttpHeaderModel() { Name = "TransactionIdentity", Value = transactionIdentity });

                return await _departmentCommunicator.CallAsync<AccountingCreateSalaryPaymentCommandRequest, Object>(authenticatedEndpoint, request, cancellationTokenSource);
            }
            else
                throw new EndpointNotFoundException();
        }

        public async Task<ServiceResultModel> RemoveSessionIfExistsInCacheAsync(
            string tokenKey,
            CancellationTokenSource cancellationTokenSource)
        {
            CachedServiceModel service = await _serviceDiscoverer.GetServiceAsync("Services.Api.Business.Departments.Accounting", cancellationTokenSource);

            IEndpoint endpoint = service.GetEndpoint(RemoveSessionIfExistsInCacheEndpoint.Path);

            if (endpoint != null)
            {
                string token = await _departmentCommunicator.GetServiceToken(cancellationTokenSource);

                IAuthenticatedEndpoint authenticatedEndpoint = endpoint.ConvertToAuthenticatedEndpoint(new TokenAuthentication(token));
                authenticatedEndpoint.Queries.Add(new HttpQueryModel() { Name = "tokenKey", Value = tokenKey });

                return await _departmentCommunicator.CallAsync<Object>(authenticatedEndpoint, cancellationTokenSource);
            }
            else
                throw new EndpointNotFoundException();
        }

        /// <summary>
        /// Kaynakları serbest bırakır
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Kaynakları serbest bırakır
        /// </summary>
        /// <param name="disposing">Kaynakların serbest bırakılıp bırakılmadığı bilgisi</param>
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!disposed)
                {

                }

                disposed = true;
            }
        }
    }
}
