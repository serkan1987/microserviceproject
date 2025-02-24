﻿using Infrastructure.Transaction.UnitOfWork.Sql;

using Microsoft.Extensions.Configuration;

using System;
using System.Diagnostics;

namespace Services.Api.Logging.Configuration.Persistence
{
    public class UnitOfWork : SqlUnitOfWork
    {
        private readonly IConfiguration _configuration;

        public UnitOfWork(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public override string ConnectionString
        {
            get
            {
                return
                    Convert.ToBoolean(_configuration.GetSection("Persistence").GetSection("Databases").GetSection("Microservice_Logs_DB")["IsSensitiveData"] ?? false.ToString())
                    &&
                    !Debugger.IsAttached
                    ?
                    Environment.GetEnvironmentVariable(_configuration.GetSection("Persistence").GetSection("Databases").GetSection("Microservice_Logs_DB")["EnvironmentVariableName"])
                    :
                    _configuration.GetSection("Persistence").GetSection("Databases").GetSection("Microservice_Logs_DB")["ConnectionString"];
            }
        }
    }
}
