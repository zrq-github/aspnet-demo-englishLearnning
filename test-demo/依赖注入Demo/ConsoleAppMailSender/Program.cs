using ConfigServices;
using LogServices;
using MailServices;
using Microsoft.Extensions.DependencyInjection;
namespace ConsoleAppMailSender
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<IConfigServices, EnvServices>();
            services.AddScoped<IMailServices,MailService>();
            services.AddScoped<ILogProvider, ConsoleLogProvider>();
            
            using var sp = services.BuildServiceProvider();
            var mailServer = sp.GetRequiredService<IMailServices>();
            mailServer.Send("Hello", "邮箱", "zrq");


            Console.WriteLine("Hello, World!");
        }
    }
}
