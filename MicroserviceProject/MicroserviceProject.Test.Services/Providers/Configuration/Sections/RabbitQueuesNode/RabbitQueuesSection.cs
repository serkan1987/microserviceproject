﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

using System;
using System.Collections.Generic;

namespace MicroserviceProject.Test.Services.Providers.Configuration.Sections.RabbitQueuesNode
{
    public class RabbitQueuesSection : BaseSection, IConfigurationSection
    {
        public ServicesSection ServicesSection { get; set; } = new ServicesSection();
        public IEnumerable<IConfigurationSection> GetChildren()
        {
            return new List<IConfigurationSection>()
            {
                ServicesSection
            };
        }
        public IChangeToken GetReloadToken()
        {
            return new RabbitQueuesChangeToken();
        }
        public IConfigurationSection GetSection(string key)
        {
            switch (key)
            {
                case "Services": return ServicesSection;
                default:
                    return null;
            }
        }
        public class RabbitQueuesChangeToken : IChangeToken
        {
            public bool HasChanged { get; }
            public bool ActiveChangeCallbacks { get; }
            public IDisposable RegisterChangeCallback(Action<object> callback, object state)
            {
                return new RabbitQueuesDisposable();
            }
            public class RabbitQueuesDisposable : IDisposable
            {
                public void Dispose() { }
            }
        }
    }
}