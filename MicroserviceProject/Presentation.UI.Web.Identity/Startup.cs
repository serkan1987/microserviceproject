using Infrastructure.Caching.InMemory.DI;
using Infrastructure.Communication.Http.Endpoint.Abstract;
using Infrastructure.Security.Authentication.DI;
using Infrastructure.ServiceDiscovery.Register.DI;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Services.Communication.Http.Broker.Authorization.DI;
using Services.Communication.Http.Endpoint.Presentation.UI.Web.Identity;
using Services.Logging.Exception.DI;
using Services.ServiceDiscovery.DI;
using Services.Util.Exception.Handlers;

using System.Collections.Generic;

namespace Presentation.UI.Web.Identity
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
            services.AddControllersWithViews();

            services.RegisterCredentialProvider();
            services.RegisterExceptionLogger();
            services.RegisterHttpAuthorizationCommunicators();
            services.RegisterInMemoryCaching();
            services.RegisterServiceRegisterers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseGlobalExceptionHandler(defaultApplicationName: "Presentation.UI.Web.Identity");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.RegisterService(new List<IEndpoint>()
            {
                new LoginEndpoint()
            });
        }
    }
}
