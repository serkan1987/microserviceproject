﻿using System;

namespace Services.Api.Authorization.Entities.EntityFramework
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
