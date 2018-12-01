using Microsoft.AspNetCore.Builder;

namespace CheckoutChallenge.Application.Configuration
{
    public interface IConfigureAspNetAppBuilder
    {
        void Configure(IApplicationBuilder app);
    }
}
