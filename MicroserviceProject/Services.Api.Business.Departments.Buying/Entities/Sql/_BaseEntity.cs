﻿using System;

namespace Services.Api.Business.Departments.Buying.Entities.Sql
{
    /// <summary>
    /// Entity tablolar için temel sınıf
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Tablolar için ortak Id değeri
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Kaydın silinme tarihi
        /// </summary>
        public DateTime? DeleteDate { get; set; }
    }
}
