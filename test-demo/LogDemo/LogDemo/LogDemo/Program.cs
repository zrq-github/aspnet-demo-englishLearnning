﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace LogDemo;

internal class Program
{
    private static void ConsoleLogger()
    {
        // 通过DI的注册日志系统
        var services = new ServiceCollection();
        services.AddLogging(logBuilder =>
        {
            logBuilder.AddConsole();
            logBuilder.SetMinimumLevel(LogLevel.Trace);
        });
        services.AddScoped<LogTestClass>();
        using var scope = services.BuildServiceProvider();
        var test1 = scope.GetRequiredService<LogTestClass>();
        test1.ConsoleLog();
    }

    private static void NLog()
    {
        var logger = LogManager.GetCurrentClassLogger();
        try
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            using var servicesProvider = new ServiceCollection()
                .AddTransient<Runner>() // Runner is the custom class
                .AddLogging(loggingBuilder =>
                {
                    // configure Logging with NLog
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                    loggingBuilder.AddNLog(config);
                }).BuildServiceProvider();

            var runner = servicesProvider.GetRequiredService<Runner>();
            runner.DoAction("Action1");

            Console.WriteLine("Press ANY key to exit");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            // NLog: catch any exception and log it.
            logger.Error(ex, "Stopped program because of exception");
            throw;
        }
        finally
        {
            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            LogManager.Shutdown();
        }
    }

    private static void Main(string[] args)
    {
        using var servicesProvider = new ServiceCollection()
            .AddTransient<Runner>() // Runner is the custom class
            .AddLogging(loggingBuilder =>
            {
                // configure Logging with NLog
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                loggingBuilder.AddNLog();
            }).BuildServiceProvider();

        var runner = servicesProvider.GetRequiredService<Runner>();
        runner.NLogTest();

        Console.WriteLine("Press ANY key to exit");
        Console.ReadKey();

        //ConsoleLogger();
        //NLog();
    }
}
