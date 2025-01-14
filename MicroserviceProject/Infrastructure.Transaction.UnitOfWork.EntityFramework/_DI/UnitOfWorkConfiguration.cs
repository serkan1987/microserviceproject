﻿using Infrastructure.Transaction.UnitOfWork.EntityFramework;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Services.UnitOfWork.EntityFramework.DI
{
    /// <summary>
    /// İş birimi DI sınıfı
    /// </summary>
    public static class UnitOfWorkConfiguration
    {
        /// <summary>
        /// İş birimini enjekte eder
        /// </summary>
        /// <param name="services">DI servisleri nesnesi</param>
        /// <returns></returns>
        public static IServiceCollection RegisterEntityFrameworkUnitOfWork<TContext>(this IServiceCollection services) where TContext:DbContext
        {
            services.AddScoped<IEfUnitOfWork<TContext>, EfUnitOfWork<TContext>>();

            return services;
        }
    }
}
