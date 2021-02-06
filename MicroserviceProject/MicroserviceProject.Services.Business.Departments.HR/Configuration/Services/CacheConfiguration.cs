﻿using MicroserviceProject.Infrastructure.Caching.Redis;

using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceProject.Services.Business.Departments.HR.Configuration.Services
{
    /// <summary>
    /// Cache işlemleri DI sınıfı
    /// </summary>
    public static class CacheConfiguration
    {
        /// <summary>
        /// Cacheleri enjekte eder
        /// </summary>
        /// <param name="services">DI servisleri nesnesi</param>
        /// <returns></returns>
        public static IServiceCollection RegisterCaching(this IServiceCollection services)
        {
            services.AddSingleton<CacheDataProvider>();

            return services;
        }
    }
}
