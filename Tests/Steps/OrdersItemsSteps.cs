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
    public class OrderItemsSteps
    {
        private readonly IOrderingClient serviceClient;

        public OrderItemsSteps(IOrderingClient serviceClient)
        {
            this.serviceClient = serviceClient;
        }

        [Then(@"the (.*) order has no items")]
        public async Task ThenTheXOrderHasNoItems(string orderName)
        {
            var order = OrdersSteps.GetStoredOrder(orderName);
            var orderItems = await order.GetItems(CancellationToken.None);
            orderItems.Should().NotBeNull().And.BeEmpty();
        }
    }
}
