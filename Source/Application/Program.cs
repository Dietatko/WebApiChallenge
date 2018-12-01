using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CheckoutChallenge.Application.Configuration;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CheckoutChallenge.Application
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await BuildHost(args).RunAsync();
        }

        private static IWebHost BuildHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddTransient<IConfigureServices, InMemoryDataModule>();
                    services.AddTransient<IConfigureServices, WebApiModule>();
                    services.AddTransient<IConfigureAspNetAppBuilder, WebApiModule>();
                })
                .UseStartup<Startup>()
                .Build();
        }
    }
}
