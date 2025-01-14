﻿using Infrastructure.Logging.Abstraction;
using Infrastructure.Logging.File.Loggers;
using Infrastructure.Logging.Managers;
using Infrastructure.Logging.MongoDb.Loggers;

using Microsoft.Extensions.Configuration;

using Services.Api.Logging.Configuration.Logging;
using Services.Logging.RequestResponse.Configuration;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Api.Logging.Util.Logging.Loggers
{
    /// <summary>
    /// Request-Response loglarını yazan sınıf
    /// </summary>
    public class RequestResponseLogger : IDisposable
    {
        /// <summary>
        /// Kaynakların serbest bırakılıp bırakılmadığı bilgisi
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Log yazma yönetimini gerçekleştiren sınıf
        /// </summary>
        private readonly LogManager<RequestResponseLogModel> _logManager;

        /// <summary>
        /// Request-Response loglarını yazan sınıf
        /// <paramref name="configuration">Request-response log ayarlarının çekileceği configuration</paramref>
        /// </summary>
        public RequestResponseLogger(IConfiguration configuration)
        {
            List<ILogger<RequestResponseLogModel>> loggers = new List<ILogger<RequestResponseLogModel>>();

            JsonFileLogger<RequestResponseLogModel> jsonFileLogger =
                new JsonFileLogger<RequestResponseLogModel>(
                    new Configuration.Logging.RequestResponseLogFileConfiguration(configuration));

            loggers.Add(jsonFileLogger);

            DefaultLogger<RequestResponseLogModel> defaultMongoLogger =
                new DefaultLogger<RequestResponseLogModel>(
                    new RequestResponseLogMongoConfiguration(configuration));

            loggers.Add(defaultMongoLogger);

            _logManager = new LogManager<RequestResponseLogModel>(loggers);
        }

        /// <summary>
        /// Log yazar
        /// </summary>
        /// <param name="model">Yazılacak request-response log modeli</param>
        public async Task LogAsync(RequestResponseLogModel model, CancellationTokenSource cancellationTokenSource)
        {
            await _logManager.LogAsync(model, cancellationTokenSource);
        }

        /// <summary>
        /// Kaynakları serbest bırakır
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Kaynakları serbest bırakır
        /// </summary>
        /// <param name="disposing">Kaynakların serbest bırakılıp bırakılmadığı bilgisi</param>
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!disposed)
                {
                    _logManager.Dispose();
                }

                disposed = true;
            }
        }
    }
}
