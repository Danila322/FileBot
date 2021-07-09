using FileBot.Services.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace FileBot
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            IHost host =  CreateHostBuilder(args).Build();

            await Initialize(host.Services);
            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task Initialize(IServiceProvider provider)
        {
            using(var scope = provider.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetService<IInitializer>();

                await initializer.Initialize();
            }            
        }
    }
}
