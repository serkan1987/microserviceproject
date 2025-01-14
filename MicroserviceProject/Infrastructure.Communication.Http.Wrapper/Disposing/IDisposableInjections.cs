﻿namespace Infrastructure.Communication.Http.Wrapper
{
    /// <summary>
    /// Enjekte edilmiş nesneleri dispose edecek arayüz
    /// </summary>
    public interface IDisposableInjections
    {
        /// <summary>
        /// DI nesneslerini dispose eder
        /// </summary>
        void DisposeInjections();
    }
}
