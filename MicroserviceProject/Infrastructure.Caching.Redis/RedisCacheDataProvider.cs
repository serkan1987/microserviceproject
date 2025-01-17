﻿using Infrastructure.Caching.Abstraction;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

using StackExchange.Redis;

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Infrastructure.Caching.Redis
{
    /// <summary>
    /// Rediste tutulan önbellek yönetimini sağlar
    /// </summary>
    public class RedisCacheDataProvider : IDistrubutedCacheProvider, IDisposable
    {
        /// <summary>
        /// Kaynakların serbest bırakılıp bırakılmadığı bilgisi
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Yapılandırma ayarları için configuration nesnesi
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Redis veritabanı nesnesi
        /// </summary>
        private readonly IDatabase _database;

        /// <summary>
        /// Rediste tutulan önbellek yönetimini sağlar
        /// </summary>
        /// <param name="configuration">Yapılandırma ayarları için configuration nesnesi</param>
        public RedisCacheDataProvider(IConfiguration configuration)
        {
            _configuration = configuration;

            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(ConnectionString);
            this._database = connection.GetDatabase();
        }

        /// <summary>
        /// Redis bağlantı cümlesini verir
        /// </summary>
        private string ConnectionString
        {
            get
            {
                return
                    Convert.ToBoolean(
                        _configuration.GetSection("Caching").GetSection("Redis")["IsSensitiveData"] ?? false.ToString()) && !Debugger.IsAttached
                        ?
                        Environment.GetEnvironmentVariable(_configuration.GetSection("Caching").GetSection("Redis")["EnvironmentVariableName"])
                        :
                        _configuration.GetSection("Caching").GetSection("Redis")["Server"];
            }
        }

        /// <summary>
        /// Önbellekteki bir listeye yeni kayıt ekler
        /// </summary>
        /// <typeparam name="T">Listenin tipi</typeparam>
        /// <param name="key">Önbellek anahtarı</param>
        /// <param name="item">Eklenecek kaydı nesnesi</param>
        public void AddItemToList<T>(string key, T item)
        {
            List<T> list = Get<List<T>>(key);

            if (list != null)
            {
                list.Add(item);
                Set<List<T>>(key, list);
            }
        }

        /// <summary>
        /// Önbellekten bir veriyi getirir
        /// </summary>
        /// <typeparam name="T">Getirilecek verinin tipi</typeparam>
        /// <param name="key">Önbellek anahtarı</param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            RedisValue cacheItem = this._database.StringGet(key);

            if (!cacheItem.IsNull && cacheItem.HasValue)
            {
                return JsonConvert.DeserializeObject<T>(cacheItem);
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Önbellekteki bir listeden kayıt siler
        /// </summary>
        /// <typeparam name="T">Listenin tipi</typeparam>
        /// <param name="key">Önbellek anahtarı</param>
        /// <param name="item">Silinecek öğe</param>
        public void RemoveItemOnList<T>(string key, T item)
        {
            List<T> list = Get<List<T>>(key);

            if (list != null && list.Contains(item))
            {
                list.Remove(item);
                Set<List<T>>(key, list);
            }
        }

        /// <summary>
        /// Önbellekten bir veriyi siler
        /// </summary>
        /// <param name="key">Silinecek önbelleğin anahtarı</param>
        public void RemoveObject(string key)
        {
            this._database.KeyDelete(key);
        }

        /// <summary>
        /// Önbelleğe bir kayıt ekler
        /// </summary>
        /// <typeparam name="T">Eklenecek kaydın tipi</typeparam>
        /// <param name="key">Önbelleğin anahtarı</param>
        /// <param name="item">Eklenecek nesne</param>
        public void Set<T>(string key, T item)
        {
            this._database.StringSet(
                key: key,
                value: JsonConvert.SerializeObject(item),
                expiry: new TimeSpan(hours: 0, minutes: 15, seconds: 0));
        }

        /// <summary>
        /// Önbelleğe bir kayıt ekler
        /// </summary>
        /// <typeparam name="T">Eklenecek kaydın tipi</typeparam>
        /// <param name="key">Önbelleğin anahtarı</param>
        /// <param name="item">Eklenecek nesne</param>
        /// <param name="toTime">Önbellekte tutulacak süre</param>
        public void Set<T>(string key, T item, DateTime toTime)
        {
            this._database.StringSet(key, JsonConvert.SerializeObject(item), toTime.TimeOfDay);
        }

        /// <summary>
        /// Önbellekten veri getirmeye çalışır
        /// </summary>
        /// <typeparam name="T">Getirilecek verinin tipi</typeparam>
        /// <param name="key">Önbellek anahtarı</param>
        /// <param name="item">Önbellekten getirilen veri</param>
        /// <returns></returns>
        public bool TryGetValue<T>(string key, out T item)
        {
            try
            {
                item = Get<T>(key);
                return true;
            }
            catch
            {
                item = default(T);
                return false;
            }
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

                }

                disposed = true;
            }
        }
    }
}
