﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Services.Api.Business.Departments.Storage.Configuration.Persistence;

using System;
using System.Diagnostics;

namespace Services.Api.Business.Departments.Storage.DI
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

            services.AddDbContext<StorageContext>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(
                    connectionString:
                        Convert.ToBoolean(
                            configuration
                            .GetSection("Persistence")
                            .GetSection("Databases")
                            .GetSection("Microservice_Storage_DB")["IsSensitiveData"] ?? false.ToString()) && !Debugger.IsAttached
                            ?
                            Environment.GetEnvironmentVariable(
                                configuration
                                .GetSection("Persistence")
                                .GetSection("Databases")
                                .GetSection("Microservice_Storage_DB")["EnvironmentVariableName"])
                            :
                            configuration
                            .GetSection("Persistence")
                            .GetSection("Databases")
                            .GetSection("Microservice_Storage_DB")["ConnectionString"]);

                optionBuilder.EnableSensitiveDataLogging();
                optionBuilder.EnableDetailedErrors();
            });

            return services;
        }
    }
}
