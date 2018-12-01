using Microsoft.Extensions.DependencyInjection;

namespace CheckoutChallenge.Application.Configuration
{
    public interface IConfigureServices
    {
        void ConfigureServices(IServiceCollection services);
    }
}
