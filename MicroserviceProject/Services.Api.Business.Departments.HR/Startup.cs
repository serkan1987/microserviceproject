using Infrastructure.Communication.Http.Endpoint.Abstract;
using Infrastructure.Diagnostics.HealthCheck.Util;
using Infrastructure.Localization.Translation.Provider.DI;
using Infrastructure.ServiceDiscovery.Register.DI;
using Infrastructure.Util.DI;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Services.Api.Business.Departments.HR.DI;
using Services.Communication.Http.Broker.Authorization.DI;
using Services.Communication.Http.Broker.Department.AA.DI;
using Services.Communication.Http.Broker.Department.Accounting.DI;
using Services.Communication.Http.Broker.Department.HR.DI;
using Services.Communication.Http.Broker.Department.IT.DI;
using Services.Communication.Mq.Queue.AA.DI;
using Services.Communication.Mq.Queue.AA.Rabbit.DI;
using Services.Communication.Mq.Queue.Accounting.DI;
using Services.Communication.Mq.Queue.Accounting.Rabbit.DI;
using Services.Communication.Mq.Queue.IT.DI;
using Services.Communication.Mq.Queue.IT.Rabbit.DI;
using Services.Diagnostics.HealthCheck.DI;
using Services.Logging.Aspect.DI;
using Services.Logging.Exception.DI;
using Services.Logging.RequestResponse.DI;
using Services.Security.BasicToken.DI;
using Services.ServiceDiscovery.DI;
using Services.Util.Exception.Handlers;

using System.Collections.Generic;

namespace Services.Api.Business.Departments.HR
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.RegisterBusinessServices();
            services.RegisterMappings();
            services.RegisterRepositories();
            services.RegisterValidators();

            services.RegisterAAQueueConfigurations();
            services.RegisterAAQueuePublishers();
            services.RegisterAccountingQueueConfigurations();
            services.RegisterAccountingQueuePublishers();
            services.RegisterBasicTokenAuthentication();
            services.RegisterExceptionLogger();
            services.RegisterHttpAADepartmentCommunicators();
            services.RegisterHttpAuthorizationCommunicators();
            services.RegisterHttpAccountingDepartmentCommunicators();
            services.RegisterHttpHRDepartmentCommunicators();
            services.RegisterHttpITDepartmentCommunicators();
            services.RegisterITQueueConfigurations();
            services.RegisterITQueuePublishers();
            services.RegisterLocalizationProviders();
            services.RegisterRequestResponseLogger();
            services.RegisterRuntimeHandlers();
            services.RegisterSqlHealthChecking();
            services.RegisterSwagger();
            services.RegisterServiceRegisterers();

            services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(Startup).Assembly));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseGlobalExceptionHandler(defaultApplicationName: "Services.Api.Business.Departments.HR");

            app.UseMiddleware<Middleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    ResponseWriter = HealthHttpResponse.WriteHealthResponse,
                });
            });


            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/CoreSwagger/swagger.json", "CoreSwagger");
            });

            app.RegisterService(new List<IEndpoint>()
            {
                new global::Services.Communication.Http.Endpoint.Department.HR.CreateDepartmentEndpoint(),
                new global::Services.Communication.Http.Endpoint.Department.HR.CreatePersonEndpoint (),
                new global::Services.Communication.Http.Endpoint.Department.HR.CreateTitleEndpoint(),
                new global::Services.Communication.Http.Endpoint.Department.HR.CreateWorkerEndpoint (),
                new global::Services.Communication.Http.Endpoint.Department.HR.GetDepartmentsEndpoint(),
                new global::Services.Communication.Http.Endpoint.Department.HR.GetPeopleEndpoint(),
                new global::Services.Communication.Http.Endpoint.Department.HR.GetTitlesEndpoint  (),
                new global::Services.Communication.Http.Endpoint.Department.HR.GetWorkersEndpoint(),
                new global::Services.Communication.Http.Endpoint.Department.HR.RemoveSessionIfExistsInCacheEndpoint(),
                new global::Services.Communication.Http.Endpoint.Department.HR.HealthCheckEndpoint()
            });
        }
    }
}
