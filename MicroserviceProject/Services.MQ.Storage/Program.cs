
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Services.Communication.Mq.Queue.Storage.Rabbit.Consumers;

using System.Threading;

namespace Services.MQ.Storage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            DescendProductStockConsumer descendProductStockConsumer =
                    (DescendProductStockConsumer)host.Services.GetService(typeof(DescendProductStockConsumer));

            descendProductStockConsumer.StartConsumeAsync(new CancellationTokenSource());

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
