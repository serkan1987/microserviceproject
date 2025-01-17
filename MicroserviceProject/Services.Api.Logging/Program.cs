using Services.Api.Logging.DI;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Services.Api.Logging
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            host.RegisterConsumers();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
