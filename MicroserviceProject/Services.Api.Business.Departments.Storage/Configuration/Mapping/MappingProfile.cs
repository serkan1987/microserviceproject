﻿using AutoMapper;

using Services.Communication.Http.Broker.Department.Storage.Models;

using Services.Api.Business.Departments.Storage.Entities.EntityFramework;

namespace Services.Api.Business.Departments.Storage.Configuration.Mapping
{
    /// <summary>
    /// Mapping profili sınıfı
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Mapping profili sınıfı
        /// </summary>
        public MappingProfile()
        {
            // Model => Entity

            CreateMap<StockModel, StockEntity>();

            // Entity => Model

            CreateMap<StockEntity, StockModel>();
        }
    }
}
