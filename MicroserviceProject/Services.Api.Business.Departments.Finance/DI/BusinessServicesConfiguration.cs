﻿using Services.Business.Departments.Finance.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Services.Business.Departments.Finance.DI
{
    /// <summary>
    /// İş mantığı servisleri DI sınıfı
    /// </summary>
    public static class BusinessServicesConfiguration
    {
        /// <summary>
        /// İş mantığı servislerini enjekte eder
        /// </summary>
        /// <param name="services">DI servisleri nesnesi</param>
        /// <returns></returns>
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<CostService>();
            services.AddScoped<ProductionRequestService>();

            return services;
        }
    }
}
