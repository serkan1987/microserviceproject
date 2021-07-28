﻿using Communication.Http.Authorization;
using Communication.Http.Department.AA;
using Communication.Http.Department.Accounting;
using Communication.Http.Department.Buying;
using Communication.Http.Department.Finance;
using Communication.Http.Department.HR;
using Communication.Http.Department.IT;

using Microsoft.Extensions.DependencyInjection;

namespace Communication.Http.DI
{
    /// <summary>
    /// İletişimcilerin DI sınıfı
    /// </summary>
    public static class CommunicatorConfiguration
    {
        /// <summary>
        /// İletişimcileri enjekte eder
        /// </summary>
        /// <param name="services">DI servisleri nesnesi</param>
        /// <returns></returns>
        public static IServiceCollection RegisterCommunicators(this IServiceCollection services)
        {
            services.AddSingleton<AuthorizationCommunicator>();
            services.AddSingleton<AACommunicator>();
            services.AddSingleton<AccountingCommunicator>();
            services.AddSingleton<BuyingCommunicator>();
            services.AddSingleton<FinanceCommunicator>();
            services.AddSingleton<HRCommunicator>();
            services.AddSingleton<ITCommunicator>();

            return services;
        }
    }
}
