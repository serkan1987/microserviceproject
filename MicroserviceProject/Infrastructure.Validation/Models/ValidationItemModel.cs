﻿namespace Infrastructure.Validation.Models
{
    /// <summary>
    /// Doğrulamaya ait detay
    /// </summary>
    public class ValidationItemModel
    {
        /// <summary>
        /// Doğrulanmaya çalışılan anahtar
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Doğrulanmaya çalışılan değer
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Doğrulama sonucu
        /// </summary>
        public string Message { get; set; }
    }
}
