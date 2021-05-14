﻿using Infrastructure.Communication.Moderator;
using Infrastructure.Routing.Persistence.Repositories.Sql;
using Infrastructure.Routing.Providers;
using Infrastructure.Security.Providers;

using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Mock.Factories
{
    /// <summary>
    /// Servis iletişim sağlayıcısını taklit eden sınıf
    /// </summary>
    public class ServiceCommunicatorFactory
    {
        /// <summary>
        /// Servis iletişim sağlayıcısı
        /// </summary>
        private static ServiceCommunicator serviceCommunicator = null;

        /// <summary>
        /// Servis iletişim sağlayıcısını verir
        /// </summary>
        /// <param name="memoryCache">Önbellek nesnesi</param>
        /// <param name="credentialProvider">Kullanıcı bilgi sağlayıcısının nesnesi</param>
        /// <param name="routeNameProvider">Servis rota isimleri sağlayıcısının nesnesi</param>
        /// <param name="serviceRouteRepository">Servis rota repository sınıfı nesnesi</param>
        /// <returns></returns>
        public static ServiceCommunicator GetServiceCommunicator(
            IMemoryCache memoryCache,
            CredentialProvider credentialProvider,
            RouteNameProvider routeNameProvider,
            ServiceRouteRepository serviceRouteRepository)
        {
            if (serviceCommunicator == null)
            {
                serviceCommunicator = new ServiceCommunicator(memoryCache, credentialProvider, routeNameProvider, serviceRouteRepository);
            }

            return serviceCommunicator;
        }
    }
}