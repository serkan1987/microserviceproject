﻿using AutoMapper;

using Services.Communication.Http.Broker.Department.Selling.Models;

using Services.Api.Business.Departments.Selling.Entities.EntityFramework;

namespace Services.Api.Business.Departments.Selling.Configuration.Mapping
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

            CreateMap<SellModel, SellEntity>();

            // Entity => Model

            CreateMap<SellEntity, SellModel>();
        }
    }
}
