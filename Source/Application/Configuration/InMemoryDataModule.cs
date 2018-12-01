using CheckoutChallenge.Domain.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace CheckoutChallenge.Application.Configuration
{
    public class InMemoryDataModule : IConfigureServices
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IOrderingRepository, InMemoryOrderingRepository>();
        }
    }
}
