﻿using Services.Api.Business.Departments.HR.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Services.Api.Business.Departments.HR.DI
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
            services.AddScoped<DepartmentService>();
            services.AddScoped<PersonService>();

            return services;
        }
    }
}
