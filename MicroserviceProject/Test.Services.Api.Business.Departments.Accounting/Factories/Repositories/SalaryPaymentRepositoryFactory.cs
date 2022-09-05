﻿using Infrastructure.Transaction.UnitOfWork.Sql;

using Microsoft.Extensions.Configuration;

using Services.Api.Business.Departments.Accounting.Configuration.Persistence;
using Services.Api.Business.Departments.Accounting.Repositories.Sql;

using Test.Services.Api.Business.Departments.Accounting.Factories.Infrastructure;

namespace Test.Services.Api.Business.Departments.Accounting.Factories.Repositories
{
    public class SalaryPaymentRepositoryFactory
    {
        private static SalaryPaymentRepository repository;

        public static SalaryPaymentRepository Instance
        {
            get
            {
                if (repository == null)
                {
                    IConfiguration configuration = ConfigurationFactory.GetConfiguration();

                    IUnitOfWork unitOfWork = new UnitOfWork(configuration);

                    repository = new SalaryPaymentRepository(unitOfWork);
                }

                return repository;
            }
        }
    }
}
