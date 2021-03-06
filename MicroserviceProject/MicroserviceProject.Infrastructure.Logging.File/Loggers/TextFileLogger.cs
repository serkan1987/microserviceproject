﻿using MicroserviceProject.Infrastructure.Logging.Abstraction;
using MicroserviceProject.Infrastructure.Logging.File.Configuration;
using MicroserviceProject.Infrastructure.Logging.Model;

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceProject.Infrastructure.Logging.File.Loggers
{
    /// <summary>
    /// Düz metin formatta log yazan sınıf
    /// </summary>
    /// <typeparam name="TModel">Yazılacak logun tipi</typeparam>
    public class TextFileLogger<TModel> : ILogger<TModel>, IDisposable where TModel : BaseLogModel, new()
    {
        /// <summary>
        /// Kaynakların serbest bırakılıp bırakılmadığı bilgisi
        /// </summary>
        protected bool Disposed = false;

        /// <summary>
        /// Yazılacak log dosyasının yapılandırması
        /// </summary>
        private IFileConfiguration _fileConfiguration;

        /// <summary>
        /// Düz metin formatta log yazan sınıf
        /// </summary>
        /// <param name="fileConfiguration">Yazılacak log dosyasının yapılandırması</param>
        public TextFileLogger(IFileConfiguration fileConfiguration)
        {
            _fileConfiguration = fileConfiguration;
        }

        /// <summary>
        /// Düz metin log yazar
        /// </summary>
        /// <param name="model">Yazılacak logun modeli</param>
        public async Task LogAsync(TModel model, CancellationToken cancellationToken)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_fileConfiguration.Path);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            await System.IO.File.AppendAllTextAsync(
                path: _fileConfiguration.Path + "\\" + _fileConfiguration.FileName,
                contents: model.ToString(),
                encoding: _fileConfiguration.Encoding,
                cancellationToken: cancellationToken);
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
                if (!Disposed)
                {
                    _fileConfiguration = null;
                }

                Disposed = true;
            }
        }
    }
}
