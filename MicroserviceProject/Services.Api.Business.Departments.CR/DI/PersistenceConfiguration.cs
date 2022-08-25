﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Services.Api.Business.Departments.CR.Configuration.Persistence;

using System;
using System.Diagnostics;

namespace Services.Api.Business.Departments.CR.DI
{
    /// <summary>
    /// Veri saklayıcıların DI sınıfı
    /// </summary>
    public static class PersistenceConfiguration
    {
        /// <summary>
        /// Veri saklayıcılarını enjekte eder
        /// </summary>
        /// <param name="services">DI servisleri nesnesi</param>
        /// <returns></returns>
        public static IServiceCollection RegisterPersistence(this IServiceCollection services)
        {
            IConfiguration configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            services.AddDbContext<CRContext>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(
                    connectionString:
                    Convert.ToBoolean(configuration.GetSection("Persistence").GetSection("Databases").GetSection("Microservice_CR_DB")["IsSensitiveData"] ?? false.ToString())
                    &&
                    !Debugger.IsAttached
                    ?
                    Environment.GetEnvironmentVariable(configuration.GetSection("Persistence").GetSection("Databases").GetSection("Microservice_CR_DB")["EnvironmentVariableName"])
                    :
                    configuration
                    .GetSection("Persistence")
                    .GetSection("Databases")
                    .GetSection("Microservice_CR_DB")["ConnectionString"]);

                optionBuilder.EnableSensitiveDataLogging();
                optionBuilder.EnableDetailedErrors();
            });

            return services;
        }
    }
}
