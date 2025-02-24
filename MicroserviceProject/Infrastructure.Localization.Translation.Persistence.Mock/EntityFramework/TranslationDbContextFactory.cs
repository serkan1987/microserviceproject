﻿using Infrastructure.Localization.Translation.Persistence.EntityFramework.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Localization.Translation.Persistence.Mock.EntityFramework.Persistence
{
    /// <summary>
    /// Dil çeviri context sınıfını taklit eden sınıf
    /// </summary>
    public class TranslationDbContextFactory
    {
        /// <summary>
        /// Dil çeviri context nesnesini verir
        /// </summary>
        /// <param name="configuration">Yapılandırma arayüzü nesnesi</param>
        /// <returns></returns>
        public static TranslationDbContext GetTranslationDbContext(IConfiguration configuration)
        {
            return new TranslationDbContext(new TestDbContextOptions(configuration));
        }
    }

    /// <summary>
    /// Dil çeviri context nesnesi için test yapılandırma sınıfı
    /// </summary>
    public class TestDbContextOptions : DbContextOptionsBuilder<TranslationDbContext>
    {
        /// <summary>
        /// Dil çeviri context nesnesi için test yapılandırma sınıfı
        /// </summary>
        /// <param name="configuration">Yapılandırma arayüzü nesnesi</param>
        public TestDbContextOptions(IConfiguration configuration) : base()
        {
            this.UseSqlServer(
                configuration
                .GetSection("Configuration")
                .GetSection("Localization")["TranslationDbConnnectionString"]);
        }
    }
}
