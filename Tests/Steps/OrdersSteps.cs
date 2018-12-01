using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CheckoutChallenge.Client;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace CheckoutChallenge.AcceptanceTests.Steps
{
    [Binding]
    public class OrdersSteps
    {
        private readonly IOrderingClient serviceClient;

        public OrdersSteps(IOrderingClient serviceClient)
        {
            this.serviceClient = serviceClient;
        }

        [Then(@"there are no existing orders")]
        public async Task ThenThereAreNoExistingOrders()
        {
            var allOrders = await serviceClient.GetOrders(CancellationToken.None);

            allOrders.Should().BeEmpty();
        }
    }
}
