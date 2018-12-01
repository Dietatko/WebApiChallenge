using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CheckoutChallenge.Application.Configuration
{
    public class WebApiModule : IConfigureServices, IConfigureAspNetAppBuilder
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public WebApiModule(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddJsonFormatters(settings =>
                    {
                        settings.Converters.Add(new StringEnumConverter());
                        settings.Formatting = hostingEnvironment.IsDevelopment() ? Formatting.Indented : Formatting.None;
                    });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (hostingEnvironment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}
