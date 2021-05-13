﻿using System.Collections.Generic;

namespace Presentation.UI.Infrastructure.Communication.Model.Validations
{
    /// <summary>
    /// Servisten dönen doğrulama
    /// </summary>
    public class Validation
    {
        /// <summary>
        /// Doğrulamanın geçerli olup olmadığı
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Doğrulamaya ait detaylar
        /// </summary>
        public List<ValidationItemModel> ValidationItems { get; set; }
    }
}
