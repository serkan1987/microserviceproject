﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

using System;
using System.Collections.Generic;

namespace Infrastructure.Mock.Providers.Configuration.Sections.LoggingNode
{
    public class MongoConfigurationSection : BaseSection, IConfigurationSection
    {
        public IEnumerable<IConfigurationSection> GetChildren()
        {
            return null;
        }
        public IChangeToken GetReloadToken()
        {
            return new MongoConfigurationChangeToken();
        }
        public IConfigurationSection GetSection(string key)
        {
            return this;
        }
        public class MongoConfigurationChangeToken : IChangeToken
        {
            public bool HasChanged { get; }
            public bool ActiveChangeCallbacks { get; }
            public IDisposable RegisterChangeCallback(Action<object> callback, object state)
            {
                return new MongoConfigurationDisposable();
            }
            public class MongoConfigurationDisposable : IDisposable
            {
                public void Dispose() { }
            }
        }
    }
}