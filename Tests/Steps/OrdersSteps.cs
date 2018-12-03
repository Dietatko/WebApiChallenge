using System;
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
        private readonly ScenarioContext scenarioContext;
        private readonly IOrderingClient serviceClient;

        public OrdersSteps(ScenarioContext scenarioContext, IOrderingClient serviceClient)
        {
            this.scenarioContext = scenarioContext;
            this.serviceClient = serviceClient;
        }

        [Given(@"I created (.*) order")]
        [When(@"I create new (.*) order")]
        public async Task WhenICreateANewOrder(string name)
        {
            var order = await serviceClient.CreateOrder(Guid.NewGuid(), CancellationToken.None);
            scenarioContext.StoreOrder(name, order);
        }

        [Then(@"the service lists no existing orders")]
        public async Task ThenThereAreNoExistingOrders()
        {
            var allOrders = await serviceClient.FindOrders(CancellationToken.None);
            allOrders.Should().BeEmpty();
        }

        [Then(@"the service should list (1) order")]
        [Then(@"the service should list (\d+) orders")]
        public async Task ThenThereAreXOrder(int count)
        {
            var allOrders = await serviceClient.FindOrders(CancellationToken.None);
            allOrders.Should().HaveCount(count);
        }

        [Then(@"the service should list the (.*) order")]
        public async Task ThenServiceListsXOrder(string name)
        {
            var expectedOrder = scenarioContext.GetOrder(name);

            var allOrders = await serviceClient.FindOrders(CancellationToken.None);
            allOrders.Should().Contain(x => x.Id == expectedOrder.Id);
        }

        [Then(@"the service should not list (.*) order")]
        public async Task ThenServiceDoesNotListXOrder(string name)
        {
            var expectedOrder = scenarioContext.GetOrder(name);

            var allOrders = await serviceClient.FindOrders(CancellationToken.None);
            allOrders.Should().NotContain(x => x.Id == expectedOrder.Id);
        }
        
        [Then(@"the service should provide details about (.*) order")]
        public async Task ThenServiceProvidesDetailsAboutXOrder(string name)
        {
            var order = scenarioContext.GetOrder(name);
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
            var order = scenarioContext.GetOrder(name);
            await order.Clear(CancellationToken.None);
        }

        [When(@"I delete (.*) order")]
        public async Task WhenIDeleteMyOrder(string name)
        {
            var order = scenarioContext.GetOrder(name);
            await order.Delete(CancellationToken.None);
        }
    }
}
