
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Services.Communication.Mq.Queue.Accounting.Rabbit.Consumers;

using System.Threading;

namespace Services.MQ.Accounting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            CreateBankAccountConsumer assignInventoryToWorkerConsumer =
                    (CreateBankAccountConsumer)host.Services.GetService(typeof(CreateBankAccountConsumer));

            assignInventoryToWorkerConsumer.StartConsumeAsync(new CancellationTokenSource());

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
