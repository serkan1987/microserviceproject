using Hangfire;
using Hangfire.MemoryStorage;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Services.Communication.Http.Broker.ServiceDiscovery.DI;
using Services.Scheduling.Diagnostics.HealthCheck.Jobs;

using System;

namespace Services.Scheduling.Diagnostics.HealthCheck
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterHttpServiceDiscoveryCommunicators();

            services.AddHangfire(config =>
            {
                config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage();
            });

            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IServiceProvider serviceProvider,
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseHangfireDashboard();
            //app.UseHangfireServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Scheduler is running");
                });
            });

            // 1 defa çalıştırır
            backgroundJobClient.Enqueue<CheckApiServicesJob>(x => x.CheckServicesAsync());

            // her 5 dakikada bir çalıştırır
            // https://crontab.guru/ 
            recurringJobManager.AddOrUpdate(nameof(CheckApiServicesJob.CheckServicesAsync), CheckApiServicesJob.MethodJob, "5 * * * *");
        }
    }
}
