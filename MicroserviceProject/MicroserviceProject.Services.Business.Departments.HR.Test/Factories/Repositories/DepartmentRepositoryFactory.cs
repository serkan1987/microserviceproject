﻿using MicroserviceProject.Services.Business.Departments.HR.Repositories.Sql;
using MicroserviceProject.Services.Business.Departments.HR.Test.Prepreations.Infrastructure;
using MicroserviceProject.Services.UnitOfWork;

using Microsoft.Extensions.Configuration;

namespace MicroserviceProject.Services.Business.Departments.HR.Test.Factories.Repositories
{
    public class DepartmentRepositoryFactory
    {
        private static DepartmentRepository departmentRepository = null;

        public static DepartmentRepository Instance
        {
            get
            {
                if (departmentRepository == null)
                {
                    IConfiguration configurationProvider = ConfigurationFactory.GetConfiguration();

                    IUnitOfWork unitOfWork = UnitOfWorkFactory.GetInstance(configurationProvider);

                    departmentRepository = new DepartmentRepository(unitOfWork);
                }

                return departmentRepository;
            }
        }
    }
}
