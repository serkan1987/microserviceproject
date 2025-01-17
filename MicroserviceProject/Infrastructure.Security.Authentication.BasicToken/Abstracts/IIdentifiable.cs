﻿using System;

namespace Infrastructure.Security.Authentication.BasicToken.Abstracts
{
    /// <summary>
    /// Kimliği tanımlayacak arayüz
    /// </summary>
    public interface IIdentifiable
    {
        /// <summary>
        /// Kimlik tanımlayıcısı
        /// </summary>
        Guid Identifier { get; }
    }
}
