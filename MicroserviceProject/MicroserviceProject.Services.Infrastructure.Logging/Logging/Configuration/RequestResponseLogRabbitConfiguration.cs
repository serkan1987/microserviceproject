﻿using MicroserviceProject.Infrastructure.Logging.RabbitMq.Configuration;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceProject.Services.Infrastructure.Logging.Logging.Configuration
{
    /// <summary>
    /// Request-response logları için rabbit sunucusunun yapılandırma ayarları
    /// </summary>
    public class RequestResponseLogRabbitConfiguration : IRabbitConfiguration
    {
        /// <summary>
        /// Request-response log ayarlarının çekileceği configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Request-response logları için rabbit sunucusunun yapılandırma ayarları
        /// </summary>
        /// <param name="configuration">Request-response log ayarlarının çekileceği configuration</param>
        public RequestResponseLogRabbitConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;

            Host =
                _configuration
                .GetSection("Configuration")
                .GetSection("Logging")
                .GetSection("RequestResponseLogging")
                .GetSection("RabbitConfiguration")
                .GetSection("RequestResponseLogging")
                .GetSection("Host").Value;

            UserName =
                _configuration
                .GetSection("Configuration")
                .GetSection("Logging")
                .GetSection("RequestResponseLogging")
                .GetSection("RabbitConfiguration")
                .GetSection("RequestResponseLogging")
                .GetSection("UserName").Value;

            Password =
                _configuration
                .GetSection("Configuration")
                .GetSection("Logging")
                .GetSection("RequestResponseLogging")
                .GetSection("RabbitConfiguration")
                .GetSection("RequestResponseLogging")
                .GetSection("Password").Value;

            QueueName =
                _configuration
                .GetSection("Configuration")
                .GetSection("Logging")
                .GetSection("RequestResponseLogging")
                .GetSection("RabbitConfiguration")
                .GetSection("RequestResponseLogging")
                .GetSection("QueueName").Value;
        }

        /// <summary>
        /// Rabbit sunucusunun adı
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Rabbit sunucusunun kullanıcı adı
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Rabbit sunucusunun parolası
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Rabbit sunucusunun kuyruk adı
        /// </summary>
        public string QueueName { get; set; }
    }
}