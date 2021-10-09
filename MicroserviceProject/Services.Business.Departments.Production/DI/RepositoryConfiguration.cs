﻿
using Microsoft.Extensions.DependencyInjection;

using Services.Business.Departments.Production.Repositories.EntityFramework;

namespace Services.Business.Departments.Production.DI
{
    /// <summary>
    /// Repository DI sınıfı
    /// </summary>
    public static class RepositoryConfiguration
    {
        /// <summary>
        /// Repositoryleri enjekte eder
        /// </summary>
        /// <param name="services">DI servisleri nesnesi</param>
        /// <param name="configuration">Configuration nesnesi</param>
        /// <returns></returns>
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ProductRepository>();

            return services;
        }
    }
}