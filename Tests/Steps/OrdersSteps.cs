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
        private const string OrderKey = "Order";

        private readonly IOrderingClient serviceClient;

        public OrdersSteps(IOrderingClient serviceClient)
        {
            this.serviceClient = serviceClient;
        }

        [When(@"I create a new (.*) order")]
        public async Task WhenICreateANewOrder(string name)
        {
            var order = await serviceClient.CreateOrder(Guid.NewGuid(), CancellationToken.None);
            ScenarioContext.Current.Set(order, GetOrderKey(name));
        }

        [Then(@"there are no existing orders")]
        public async Task ThenThereAreNoExistingOrders()
        {
            var allOrders = await serviceClient.GetOrders(CancellationToken.None);
            allOrders.Should().BeEmpty();
        }

        [Then(@"there is (1) order")]
        [Then(@"there are (\d+) orders")]
        public async Task ThenThereAreXOrder(int count)
        {
            var allOrders = await serviceClient.GetOrders(CancellationToken.None);
            allOrders.Should().HaveCount(count);
        }

        [Then(@"the service lists (.*) order")]
        public async Task ThenServiceListsXOrder(string name)
        {
            var expectedOrder = ScenarioContext.Current.Get<Order>(GetOrderKey(name));

            var allOrders = await serviceClient.GetOrders(CancellationToken.None);
            allOrders.Should().Contain(x => x.Id == expectedOrder.Id);
        }

        [Then(@"the service provides details about (.*) order")]
        public async Task ThenServiceProvidesDetailsAboutXOrder(string name)
        {
            var expectedOrder = ScenarioContext.Current.Get<Order>(GetOrderKey(name));

            var actualOrder = await serviceClient.GetOrder(expectedOrder.Id, CancellationToken.None);
            actualOrder.Id.Should().Be(expectedOrder.Id);
            actualOrder.CustomerId.Should().Be(expectedOrder.CustomerId);
            actualOrder.CreatedAt.Should().Be(expectedOrder.CreatedAt);
            actualOrder.LastModifiedAt.Should().Be(expectedOrder.LastModifiedAt);
        }

        private string GetOrderKey(string name) => $"{nameof(Order)}_{name}";
    }
}
