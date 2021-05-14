﻿using Infrastructure.Communication.Model.Basics;
using Infrastructure.Communication.Moderator;
using Infrastructure.Routing.Exceptions;
using Infrastructure.Routing.Model;
using Infrastructure.Routing.Persistence.Repositories.Sql;
using Infrastructure.Routing.Providers;
using Infrastructure.Security.Authentication.Exceptions;
using Infrastructure.Security.Model;
using Infrastructure.Security.Providers;
using Infrastructure.Sockets.Exceptions;
using Infrastructure.Sockets.Model;
using Infrastructure.Sockets.Persistence.Repositories.Sql;
using Infrastructure.Sockets.Providers;

using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Caching.Memory;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Communication.WebSockets
{
    /// <summary>
    /// Bir websocket bağlantısını dinleyen sınıf
    /// </summary>
    public class SocketListener : IDisposable
    {
        /// <summary>
        /// Kaynakların serbest bırakılıp bırakılmadığı bilgisi
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Servis endpointlerinin önbellekteki adı
        /// </summary>
        private const string CACHEDSERVICEROUTES = "CACHED_SERVICE_ROUTES";

        /// <summary>
        /// Websocket ile iletişimde kullanılacak yetki tokenının önbellekteki saklama anahtarı
        /// </summary>
        private const string TAKENTOKENFORTHISSERVICE = "TAKEN_TOKEN_FOR_THIS_SERVICE";

        /// <summary>
        /// Servis endpointlerinin önbellekteki adı
        /// </summary>
        private const string CACHEDWEBSOCKETS = "CACHED_WEBSOCKETS";

        /// <summary>
        /// Önbellek nesnesi
        /// </summary>
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// İletişimde kullanılacak yetkiler için sağlayıcı
        /// </summary>
        private readonly CredentialProvider _credentialProvider;

        /// <summary>
        /// Gerektiğinde iletişimde bulunacak yetki servisi için rota isimleri sağlayıcısı
        /// </summary>
        private readonly RouteNameProvider _routeNameProvider;

        /// <summary>
        /// Servis endpointleri sağlayıcısı
        /// </summary>
        private readonly ServiceRouteRepository _serviceRouteRepository;

        /// <summary>
        /// Soket isimlerinin sağlayıcısı
        /// </summary>
        private readonly SocketNameProvider _socketNameProvider;

        /// <summary>
        /// Soket endpointlerinin sağlayıcısı
        /// </summary>
        private readonly SocketRepository _socketRepository;

        /// <summary>
        /// Gelen soket verisini yakalayacak handler
        /// </summary>
        /// <param name="webSocketResult">Yakalanan soket verisi</param>
        public delegate void OnMessageReceivedHandler(WebSocketResultModel webSocketResult);

        /// <summary>
        /// Soketten veri alındığında ateşlenecek olay
        /// </summary>
        public event OnMessageReceivedHandler OnMessageReceived;

        /// <summary>
        /// Bir websocket bağlantısını dinleyen sınıf
        /// </summary>
        /// <param name="memoryCache">Önbellek nesnesi</param>
        /// <param name="credentialProvider">İletişimde kullanılacak yetkiler için sağlayıcı</param>
        /// <param name="routeNameProvider">Gerektiğinde iletişimde bulunacak yetki servisi için rota isimleri sağlayıcısı</param>
        /// <param name="serviceRouteRepository">Servis endpointleri sağlayıcısı</param>
        /// <param name="socketNameProvider">Soket isimlerinin sağlayıcısı</param>
        /// <param name="socketRepository">Soket endpointlerinin sağlayıcısı</param>
        public SocketListener(
            IMemoryCache memoryCache,
            CredentialProvider credentialProvider,
            RouteNameProvider routeNameProvider,
            ServiceRouteRepository serviceRouteRepository,
            SocketNameProvider socketNameProvider,
            SocketRepository socketRepository)
        {
            _memoryCache = memoryCache;
            _credentialProvider = credentialProvider;
            _routeNameProvider = routeNameProvider;
            _serviceRouteRepository = serviceRouteRepository;
            _socketNameProvider = socketNameProvider;
            _socketRepository = socketRepository;
        }

        /// <summary>
        /// Bir web soketi dinlemeye başlar
        /// </summary>
        /// <param name="socketName">Dinlenecek soketin adı</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        public async Task ListenAsync(string socketName, CancellationTokenSource cancellationTokenSource)
        {
            Token takenTokenForThisService = _memoryCache.Get<Token>(TAKENTOKENFORTHISSERVICE);

            if (string.IsNullOrWhiteSpace(takenTokenForThisService?.TokenKey)
                ||
                takenTokenForThisService.ValidTo <= DateTime.Now)
            {
                ServiceCaller serviceTokenCaller = new ServiceCaller(_memoryCache, "");
                serviceTokenCaller.OnNoServiceFoundInCacheAsync += async (serviceName) =>
                {
                    return await GetServiceAsync(serviceName, cancellationTokenSource);
                };

                ServiceResultModel<Token> tokenResult =
                    await serviceTokenCaller.Call<Token>(
                        serviceName: _routeNameProvider.Auth_GetToken,
                        postData: new Credential()
                        {
                            Email = _credentialProvider.GetEmail,
                            Password = _credentialProvider.GetPassword
                        },
                        queryParameters: null,
                        headers: null,
                        cancellationTokenSource: cancellationTokenSource);

                if (tokenResult.IsSuccess && tokenResult.Data != null)
                {
                    takenTokenForThisService = tokenResult.Data;
                    _memoryCache.Set<Token>(TAKENTOKENFORTHISSERVICE, tokenResult.Data);
                }
                else
                {
                    throw new GetTokenException("Kaynak servis yetki tokenı elde edilemedi");
                }
            }

            SocketModel socket = await GetSocketAsync(socketName, cancellationTokenSource);

            HubConnection hubConnection = new HubConnectionBuilder().WithUrl(socket.Endpoint, options =>
            {
                options.Headers.Add("Authorization", takenTokenForThisService.TokenKey);
            }).Build();

            hubConnection.On<object>(socket.Method, param =>
            {
                if (OnMessageReceived != null)
                {
                    WebSocketResultModel webSocketResult = JsonConvert.DeserializeObject<WebSocketResultModel>(param.ToString());

                    OnMessageReceived(webSocketResult);
                }
            });

            await hubConnection.StartAsync(cancellationTokenSource.Token);
        }

        /// <summary>
        /// Servis rota bilgisini verir
        /// </summary>
        /// <param name="serviceName">Bilgisi getirilecek servisin adı</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        private async Task<string> GetServiceAsync(string serviceName, CancellationTokenSource cancellationTokenSource)
        {
            List<ServiceRouteModel> serviceRoutes = _memoryCache.Get<List<ServiceRouteModel>>(CACHEDSERVICEROUTES);

            if (serviceRoutes == null || !serviceRoutes.Any())
            {
                serviceRoutes = await _serviceRouteRepository.GetServiceRoutesAsync(cancellationTokenSource);

                _memoryCache.Set<List<ServiceRouteModel>>(CACHEDSERVICEROUTES, serviceRoutes, DateTime.Now.AddMinutes(60));
            }

            if (serviceRoutes.Any(x => x.ServiceName == serviceName))
                return JsonConvert.SerializeObject(serviceRoutes.FirstOrDefault(x => x.ServiceName == serviceName));
            else
                throw new GetRouteException("Servis rotası bulunamadı");
        }

        /// <summary>
        /// Soket bilgisini verir
        /// </summary>
        /// <param name="socketName">Bilgisi getirilecek soketin adı</param>
        /// <param name="cancellationTokenSource">İptal tokenı</param>
        /// <returns></returns>
        private async Task<SocketModel> GetSocketAsync(string socketName, CancellationTokenSource cancellationTokenSource)
        {
            List<SocketModel> sockets = _memoryCache.Get<List<SocketModel>>(CACHEDWEBSOCKETS);

            if (sockets == null || !sockets.Any())
            {
                sockets = await _socketRepository.GetSocketsAsync(cancellationTokenSource);

                _memoryCache.Set<List<SocketModel>>(CACHEDWEBSOCKETS, sockets, DateTime.Now.AddMinutes(60));
            }

            if (sockets.Any(x => x.Name == socketName))
                return sockets.FirstOrDefault(x => x.Name == socketName);
            else
                throw new GetSocketException("Soket bilgisi bulunamadı");
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
                if (!disposed)
                {
                    if (_credentialProvider != null)
                        _credentialProvider.Dispose();

                    if (_routeNameProvider != null)
                        _routeNameProvider.Dispose();

                    if (_serviceRouteRepository != null)
                        _serviceRouteRepository.Dispose();

                    if (_socketNameProvider != null)
                        _socketNameProvider.Dispose();

                    if (_socketRepository != null)
                        _socketRepository.Dispose();
                }

                disposed = true;
            }
        }
    }
}