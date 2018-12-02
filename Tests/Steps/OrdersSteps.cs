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

        [Given(@"I created (.*) order")]
        [When(@"I create new (.*) order")]
        public async Task WhenICreateANewOrder(string name)
        {
            var order = await serviceClient.CreateOrder(Guid.NewGuid(), CancellationToken.None);
            StoreOrder(name, order);
        }

        [Then(@"the service lists no existing orders")]
        public async Task ThenThereAreNoExistingOrders()
        {
            var allOrders = await serviceClient.FindOrders(CancellationToken.None);
            allOrders.Should().BeEmpty();
        }

        [Then(@"the service lists (1) order")]
        [Then(@"the service lists (\d+) orders")]
        public async Task ThenThereAreXOrder(int count)
        {
            var allOrders = await serviceClient.FindOrders(CancellationToken.None);
            allOrders.Should().HaveCount(count);
        }

        [Then(@"the service lists (.*) order")]
        public async Task ThenServiceListsXOrder(string name)
        {
            var expectedOrder = GetStoredOrder(name);

            var allOrders = await serviceClient.FindOrders(CancellationToken.None);
            allOrders.Should().Contain(x => x.Id == expectedOrder.Id);
        }

        [Then(@"the service provides details about (.*) order")]
        public async Task ThenServiceProvidesDetailsAboutXOrder(string name)
        {
            var order = GetStoredOrder(name);
            var expectedValues = new
            {
                order.Id,
                order.CustomerId,
                order.CreatedAt,
                order.LastModifiedAt,
            };
            
            await order.Update(CancellationToken.None);

            order.Id.Should().Be(expectedValues.Id);
            order.CustomerId.Should().Be(expectedValues.CustomerId);
            order.CreatedAt.Should().Be(expectedValues.CreatedAt);
            order.LastModifiedAt.Should().Be(expectedValues.LastModifiedAt);
        }

        [When(@"I clear (.*) order")]
        public async Task WhenIClearMyOrder(string name)
        {
            var order = GetStoredOrder(name);
            await order.Clear(CancellationToken.None);
        }


        public static void StoreOrder(string name, Order order)
        {
            ScenarioContext.Current.Set(order, GetOrderKey(name));
        }

        public static Order GetStoredOrder(string name)
        {
            return ScenarioContext.Current.Get<Order>(GetOrderKey(name));
        }

        private static string GetOrderKey(string name) => $"{nameof(Order)}_{name}";
    }
}
