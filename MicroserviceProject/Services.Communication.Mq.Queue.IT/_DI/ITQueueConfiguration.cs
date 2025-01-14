﻿using Microsoft.Extensions.DependencyInjection;

using Services.Communication.Mq.Queue.IT.Configuration;

namespace Services.Communication.Mq.Queue.IT.DI
{
    /// <summary>
    /// Kuyrukların DI sınıfı
    /// </summary>
    public static class ITQueueConfiguration
    {
        /// <summary>
        /// Kuyrukları enjekte eder
        /// </summary>
        /// <param name="services">DI servisleri nesnesi</param>
        /// <returns></returns>
        public static IServiceCollection RegisterITQueueConfigurations(this IServiceCollection services)
        {
            services.AddSingleton<ITAssignInventoryToWorkerRabbitConfiguration>();
            services.AddSingleton<InformInventoryRequestRabbitConfiguration>();

            return services;
        }
    }
}
