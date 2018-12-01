using System;
using BoDi;
using CheckoutChallenge.Client;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;

namespace CheckoutChallenge.AcceptanceTests.Steps
{
    [Binding]
    internal class Startup
    {
        private readonly IObjectContainer container;

        public Startup(IObjectContainer container)
        {
            this.container = container;
        }

        [BeforeScenario(Order = 0)]
        public void BeforeScenario()
        {
            var configuration = RegisterConfiguration();
            RegisterOrderingClient(configuration);
        }
        
        private IConfiguration RegisterConfiguration()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsettings.json", true, true);

            var config = configBuilder.Build();
            container.RegisterInstanceAs<IConfiguration>(config);

            return config;
        }

        private void RegisterOrderingClient(IConfiguration configuration)
        {
            container.RegisterFactoryAs<IOrderingClient>(c =>
            {
                var endpointUrl = configuration.GetValue<string>("OrderingService:Endpoint");
                if (endpointUrl == null)
                    throw new InvalidOperationException("The ordering REST API endpoint address is not specified.");

                return new OrderingClient(new Uri(endpointUrl));
            });
        }
    }
}
