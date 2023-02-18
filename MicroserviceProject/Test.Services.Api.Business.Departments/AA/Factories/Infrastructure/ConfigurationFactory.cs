﻿using Microsoft.Extensions.Configuration;

namespace Test.Services.Api.Business.Departments.AA.Factories.Infrastructure
{
    public class ConfigurationFactory
    {
        public static IConfiguration GetConfiguration()
        {
            string jsonFilePath = 
                new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName
                +
                "\\Services.Api.Business.Departments.AA";

            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(jsonFilePath)
                .AddJsonFile("appsettings.json")
                .Build();

            return configurationRoot;
        }
    }
}
