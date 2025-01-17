﻿using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Communication.Http.Broker.DI
{
    /// <summary>
    /// Servis iletişim sağlayıcı DI sınıfı
    /// </summary>
    public static class ServiceCommunicationConfiguration
    {
        /// <summary>
        /// Servis iletişim sağlayıcı sınıfı enjekte eder
        /// </summary>
        /// <param name="services">DI servisleri nesnesi</param>
        /// <returns></returns>
        public static IServiceCollection RegisterHttpServiceCommunicator(this IServiceCollection services)
        {
            services.AddSingleton<HttpGetCaller>();
            services.AddSingleton<HttpPostCaller>();
            services.AddSingleton<HttpPutCaller>();
            services.AddSingleton<HttpDeleteCaller>();

            return services;
        }
    }
}
