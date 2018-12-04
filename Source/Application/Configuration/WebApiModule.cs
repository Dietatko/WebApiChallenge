using Halcyon.Web.HAL.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
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
                .AddMvcOptions(c =>
                {
                    c.OutputFormatters.RemoveType<JsonOutputFormatter>();
                    c.OutputFormatters.Add(new JsonHalOutputFormatter(
                        new [] { "application/hal+json", "application/vnd.example.hal+json", "application/vnd.example.hal.v1+json" }
                    ));
                })
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
