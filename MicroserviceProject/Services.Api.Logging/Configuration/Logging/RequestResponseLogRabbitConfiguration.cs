﻿using Infrastructure.Communication.Mq.Configuration;

using Microsoft.Extensions.Configuration;

using System;
using System.Diagnostics;

namespace Services.Api.Logging.Configuration.Logging
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
                Convert.ToBoolean(
                    _configuration
                    .GetSection("Configuration")
                    .GetSection("Logging")
                    .GetSection("RequestResponseLogging")
                    .GetSection("RabbitConfiguration")["IsSensitiveData"] ?? false.ToString()) && !Debugger.IsAttached
                    ?
                    Environment.GetEnvironmentVariable(
                        _configuration
                        .GetSection("Configuration")
                        .GetSection("Logging")
                        .GetSection("RequestResponseLogging")
                        .GetSection("RabbitConfiguration")["EnvironmentVariableNamePrefix"] + "_Host")
                    :
                    _configuration
                    .GetSection("Configuration")
                    .GetSection("Logging")
                    .GetSection("RequestResponseLogging")
                    .GetSection("RabbitConfiguration")["Host"];

            UserName =
                Convert.ToBoolean(
                    _configuration
                    .GetSection("Configuration")
                    .GetSection("Logging")
                    .GetSection("RequestResponseLogging")
                    .GetSection("RabbitConfiguration")["IsSensitiveData"] ?? false.ToString()) && !Debugger.IsAttached
                    ?
                    Environment.GetEnvironmentVariable(
                        _configuration
                        .GetSection("Configuration")
                        .GetSection("Logging")
                        .GetSection("RequestResponseLogging")
                        .GetSection("RabbitConfiguration")["EnvironmentVariableNamePrefix"] + "_UserName")
                    :
                    _configuration
                    .GetSection("Configuration")
                    .GetSection("Logging")
                    .GetSection("RequestResponseLogging")
                    .GetSection("RabbitConfiguration")["UserName"];

            Password =
                Convert.ToBoolean(
                    _configuration
                    .GetSection("Configuration")
                    .GetSection("Logging")
                    .GetSection("RequestResponseLogging")
                    .GetSection("RabbitConfiguration")["IsSensitiveData"] ?? false.ToString()) && !Debugger.IsAttached
                    ?
                    Environment.GetEnvironmentVariable(
                        _configuration
                        .GetSection("Configuration")
                        .GetSection("Logging")
                        .GetSection("RequestResponseLogging")
                        .GetSection("RabbitConfiguration")["EnvironmentVariableNamePrefix"] + "_Password")
                    :
                    _configuration
                    .GetSection("Configuration")
                    .GetSection("Logging")
                    .GetSection("RequestResponseLogging")
                    .GetSection("RabbitConfiguration")["Password"];

            QueueName =
                _configuration
                .GetSection("Configuration")
                .GetSection("Logging")
                .GetSection("RequestResponseLogging")
                .GetSection("RabbitConfiguration")["QueueName"];
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
