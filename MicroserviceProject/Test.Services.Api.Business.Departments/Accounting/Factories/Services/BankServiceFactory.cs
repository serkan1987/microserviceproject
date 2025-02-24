﻿using AutoMapper;

using Infrastructure.Caching.Abstraction;
using Infrastructure.Caching.Redis.Mock;
using Infrastructure.Localization.Translation.Persistence.EntityFramework.Repositories;
using Infrastructure.Localization.Translation.Persistence.Mock.EntityFramework.Persistence;
using Infrastructure.Localization.Translation.Provider.Mock;
using Infrastructure.Mock.Factories;
using Infrastructure.Transaction.UnitOfWork.Sql;

using Microsoft.Extensions.Configuration;

using Services.Api.Business.Departments.Accounting.Configuration.Mapping;
using Services.Api.Business.Departments.Accounting.Configuration.Persistence;
using Services.Api.Business.Departments.Accounting.Services;

using Test.Services.Api.Business.Departments.Accounting.Factories.Infrastructure;
using Test.Services.Api.Business.Departments.Accounting.Factories.Repositories;

namespace Test.Services.Api.Business.Departments.Accounting.Factories.Services
{
    public class BankServiceFactory
    {
        public static BankService Instance
        {
            get
            {
                IConfiguration configuration = ConfigurationFactory.GetConfiguration();
                IMapper mapper = MappingFactory.GetInstance(new MappingProfile());
                IDistrubutedCacheProvider distrubutedCacheProvider = CacheDataProviderFactory.GetInstance(configuration);
                ISqlUnitOfWork unitOfWork = new UnitOfWork(configuration);

                var service = new BankService(
                    mapper: mapper,
                    unitOfWork: unitOfWork,
                    translationProvider: TranslationProviderFactory.GetTranslationProvider(
                        configuration: configuration,
                        cacheDataProvider: distrubutedCacheProvider,
                        translationRepository: new EfTranslationRepository(TranslationDbContextFactory.GetTranslationDbContext(configuration)),
                        translationHelper: TranslationHelperFactory.Instance),
                    distrubutedCacheProvider: distrubutedCacheProvider,
                    transactionItemRepository: TransactionItemRepositoryFactory.GetInstance(unitOfWork),
                    transactionRepository: TransactionRepositoryFactory.GetInstance(unitOfWork),
                    bankAccountRepository: BankAccountRepositoryFactory.GetInstance(unitOfWork),
                    currencyRepository: CurrencyRepositoryFactory.GetInstance(unitOfWork),
                    salaryPaymentRepository: SalaryPaymentRepositoryFactory.GetInstance(unitOfWork));

                service.TransactionIdentity = new Random().Next(int.MinValue, int.MaxValue).ToString();

                return service;
            }
        }
    }
}
