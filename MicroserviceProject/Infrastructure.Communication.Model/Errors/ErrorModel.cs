﻿namespace Infrastructure.Communication.Model.Errors
{
    /// <summary>
    /// Servisten dönen hata
    /// </summary>
    public class ErrorModel
    {
        /// <summary>
        /// Hatanın kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Hatanın açıklaması
        /// </summary>
        public string Description { get; set; }
    }
}