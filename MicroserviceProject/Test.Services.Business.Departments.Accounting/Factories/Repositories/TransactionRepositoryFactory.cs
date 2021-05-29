﻿using Infrastructure.Mock.Factories;
using Infrastructure.Transaction.UnitOfWork;

using Microsoft.Extensions.Configuration;

using Services.Business.Departments.Accounting.Repositories.Sql;

using Test.Services.Business.Departments.Accounting.Factories.Infrastructure;

namespace Test.Services.Business.Departments.Accounting.Factories.Repositories
{
    public class TransactionRepositoryFactory
    {
        private static TransactionRepository repository = null;

        public static TransactionRepository Instance
        {
            get
            {
                if (repository == null)
                {
                    IConfiguration configurationProvider = ConfigurationFactory.GetConfiguration();
                    IUnitOfWork unitOfWork = UnitOfWorkFactory.GetInstance(configurationProvider);

                    repository = new TransactionRepository(unitOfWork);
                }

                return repository;
            }
        }
    }
}