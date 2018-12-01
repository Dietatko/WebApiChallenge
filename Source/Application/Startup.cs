using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using CheckoutChallenge.Application.Configuration;

namespace CheckoutChallenge.Application
{
    public class Startup : StartupBase
    {
        private readonly IEnumerable<IConfigureServices> modules;
        private readonly IEnumerable<IConfigureAspNetAppBuilder> appBuilders;

        public Startup(IEnumerable<IConfigureServices> modules, IEnumerable<IConfigureAspNetAppBuilder> appBuilders)
        {
            this.modules = modules;
            this.appBuilders = appBuilders;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            foreach (var module in modules)
                module.ConfigureServices(services);
        }

        public override void Configure(IApplicationBuilder app)
        {
            foreach (var appBuilder in appBuilders)
                appBuilder.Configure(app);
        }
    }
}
