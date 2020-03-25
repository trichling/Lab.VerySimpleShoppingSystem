using System;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using NServiceBus.FluentConfiguration.Core;

namespace ShoppingSerivce
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

         public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseNServiceBus(ctx => {
                    var configurationBuilder = new ConfigureNServiceBus()
                        .WithEndpoint("ShoppingService")
                        .WithTransport<LearningTransport>()
                        .WithRouting(r => {})
                        .WithPersistence<LearningPersistence>();

                    return configurationBuilder.Configuration;
                })
                .ConfigureServices(services => {
                    
                })
                .UseConsoleLifetime();
    }
}
