using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace 依赖注入
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, 依赖注入测试!");
            var str = "123";

            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddHostedService<Worker>();
            builder.Services.AddSingleton<IMessageWriter, MessageWriter>();
            //builder.Services.AddSingleton<ILogger<Worker>>();

            using IHost host = builder.Build();

            host.Run();

            Console.WriteLine("Hello, 依赖注入测试结束");
        }

    }

    public sealed class Worker(ILogger<Worker> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await Task.Delay(1_000, stoppingToken);
            }
        }
    }

    public interface IMessageWriter
    {
        void Write(string message);
    }

    public class MessageWriter : IMessageWriter
    {
        public void Write(string message)
        {
            Console.WriteLine($"MessageWriter.Write(message: \"{message}\")");
        }
    }
}
