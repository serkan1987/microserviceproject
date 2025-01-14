﻿using Infrastructure.Logging.Model;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Logging.Abstraction
{
    /// <summary>
    /// Loglayıcı sınıfların arayüzü
    /// </summary>
    /// <typeparam name="TModel">Yazılacak logun tipi</typeparam>
    public interface ILogger<TModel> : IDisposable where TModel : BaseLogModel, new()
    {
        /// <summary>
        /// Log yazar
        /// </summary>
        /// <param name="model">Yazılacak logun modeli</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        Task LogAsync(TModel model, CancellationTokenSource cancellationTokenSource);
    }
}
