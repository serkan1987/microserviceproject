﻿using MicroserviceProject.Services.Communication.Configuration.Rabbit.AA;
using MicroserviceProject.Services.Communication.Configuration.Rabbit.Accounting;
using MicroserviceProject.Services.Communication.Configuration.Rabbit.IT;
using MicroserviceProject.Services.Communication.Publishers.AA;
using MicroserviceProject.Services.Communication.Publishers.Account;
using MicroserviceProject.Services.Communication.Publishers.IT;

using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceProject.Services.Communication.DI
{
    /// <summary>
    /// Kuyrukların DI sınıfı
    /// </summary>
    public static class QueueConfiguration
    {
        /// <summary>
        /// Kuyrukları enjekte eder
        /// </summary>
        /// <param name="services">DI servisleri nesnesi</param>
        /// <returns></returns>
        public static IServiceCollection RegisterQueues(this IServiceCollection services)
        {
            services.AddSingleton<AAAssignInventoryToWorkerRabbitConfiguration>();
            services.AddSingleton<ITAssignInventoryToWorkerRabbitConfiguration>();
            services.AddSingleton<CreateBankAccountRabbitConfiguration>();

            services.AddSingleton<AAAssignInventoryToWorkerPublisher>();
            services.AddSingleton<ITAssignInventoryToWorkerPublisher>();
            services.AddSingleton<CreateBankAccountPublisher>();

            return services;
        }
    }
}