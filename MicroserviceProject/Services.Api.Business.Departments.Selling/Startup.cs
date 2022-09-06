using Infrastructure.Communication.Http.Models;
using Infrastructure.Diagnostics.HealthCheck.Util;
using Infrastructure.Localization.Translation.Provider.DI;
using Infrastructure.Util.DI;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Newtonsoft.Json;

using Services.Api.Business.Departments.Selling.Configuration.Persistence;
using Services.Api.Business.Departments.Selling.DI;
using Services.Communication.Http.Broker.Department.Selling.DI;
using Services.Communication.Mq.Queue.Finance.DI;
using Services.Communication.Mq.Queue.Finance.Rabbit.DI;
using Services.Communication.Mq.Queue.Production.DI;
using Services.Communication.Mq.Queue.Production.Rabbit.DI;
using Services.Diagnostics.HealthCheck.DI;
using Services.Logging.Aspect.DI;
using Services.Logging.Exception;
using Services.Logging.Exception.Configuration;
using Services.Logging.Exception.DI;
using Services.Logging.RequestResponse.DI;
using Services.Security.BasicToken.DI;
using Services.UnitOfWork.EntityFramework.DI;

using System;
using System.Net;

namespace Services.Api.Business.Departments.Selling
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
            services.RegisterPersistence();
            services.RegisterRepositories();

            services.RegisterBasicTokenAuthentication();
            services.RegisterExceptionLogger();
            services.RegisterFinanceQueueConfigurations();
            services.RegisterFinanceQueuePublishers();
            services.RegisterHttpSellingDepartmentCommunicators();
            services.RegisterLocalizationProviders();
            services.RegisterProductionQueueConfigurations();
            services.RegisterProductionQueuePublishers();
            services.RegisterRequestResponseLogger();
            services.RegisterRuntimeHandlers();
            services.RegisterSqlHealthChecking();
            services.RegisterSwagger();
            services.RegisterEntityFrameworkUnitOfWork<SellingContext>();

            services.AddMediatR(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(handler =>
            {
                handler.Run(async context =>
                {
                    var error = context.Features.Get<IExceptionHandlerPathFeature>().Error;

                    await app.ApplicationServices.GetRequiredService<ExceptionLogger>().LogAsync(new ExceptionLogModel()
                    {
                        ApplicationName = Environment.GetEnvironmentVariable("ApplicationName") ?? "Services.Api.Business.Departments.Selling",
                        Date = DateTime.UtcNow,
                        MachineName = Environment.MachineName,
                        ExceptionMessage = error.Message,
                        InnerExceptionMessage = error.InnerException?.Message
                    }, null);

                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";
                    await
                    context.Response.WriteAsync(JsonConvert.SerializeObject(new ServiceResultModel()
                    {
                        IsSuccess = false,
                        ErrorModel = new ErrorModel()
                        {
                            Description =
                            error.Message
                            +
                            (error.InnerException != null ? (Environment.NewLine + error.InnerException.Message) : String.Empty)
                        }
                    }));
                });
            });

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
        }
    }
}
