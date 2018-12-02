using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace CheckoutChallenge.AcceptanceTests.Steps
{
    [Binding]
    public class OrderItemsSteps
    {
        [Then(@"the (.*) order has no items")]
        public async Task ThenTheXOrderHasNoItems(string orderName)
        {
            var order = OrdersSteps.GetStoredOrder(orderName);
            var orderItems = await order.GetItems(CancellationToken.None);
            orderItems.Should().NotBeNull().And.BeEmpty();
        }

        [Then(@"the (.*) order has (1) item")]
        [Then(@"the (.*) order has (\d*) items")]
        public async Task ThenTheMyOrderHasItem(string orderName, int count)
        {
            var order = OrdersSteps.GetStoredOrder(orderName);
            var orderItems = await order.GetItems(CancellationToken.None);
            orderItems.Should().HaveCount(count);
        }

        [Given(@"I added product ([0-9A-Fa-f\-]*) to (.*) order")]
        [When(@"I add product ([0-9A-Fa-f\-]*) to (.*) order")]
        public Task WhenIAddProductXToXOrder(Guid productId, string orderName)
        {
            return WhenIAddProductXWithAmountXToXOrder(productId, 1, orderName);
        }

        [Given(@"I added product ([0-9A-Fa-f\-]*) with amount (\d+(?:\.\d+)?) to (.*) order")]
        [When(@"I add product ([0-9A-Fa-f\-]*) with amount (\d+(?:\.\d+)?) to (.*) order")]
        public async Task WhenIAddProductXWithAmountXToXOrder(Guid productId, decimal amount, string orderName)
        {
            var order = OrdersSteps.GetStoredOrder(orderName);

            await order.CreateItem(productId, amount, CancellationToken.None);
        }

        [When(@"I update amount of product ([0-9A-Fa-f\-]*) in (.*) order to (\d+(?:\.\d+)?)")]
        public async Task WhenIUpdateAmountOfProductECEB_BDEFInMyOrderTo(Guid productId, string orderName, decimal amount)
        {
            var order = OrdersSteps.GetStoredOrder(orderName);
            var items = await order.GetItems(CancellationToken.None);

            var item = items.SingleOrDefault(x => x.ProductId == productId);
            item.Should().NotBeNull();

            item.Amount = amount;
            await item.Store(CancellationToken.None);
        }

        [When(@"I remove product ([0-9A-Fa-f\-]*) from (.*) order")]
        public async Task WhenIRemoveProductECEB_BDEFFromMyOrder(Guid productId, string orderName)
        {
            var order = OrdersSteps.GetStoredOrder(orderName);
            var items = await order.GetItems(CancellationToken.None);

            var item = items.SingleOrDefault(x => x.ProductId == productId);
            await item.Delete(CancellationToken.None);
        }


        [Then(@"the (.*) order contains product ([0-9A-Fa-f\-]*)")]
        public async Task ThenXOrderContainsProductX(string orderName, Guid productId)
        {
            var order = OrdersSteps.GetStoredOrder(orderName);
            var orderItems = await order.GetItems(CancellationToken.None);
            orderItems.Should().Contain(x => x.ProductId == productId);
        }

        [Then(@"the (.*) order contains product ([0-9A-Fa-f\-]*) with amount (\d+(?:\.\d+)?)")]
        public async Task ThenXOrderContainsProductXWithAmountX(string orderName, Guid productId, decimal amount)
        {
            var order = OrdersSteps.GetStoredOrder(orderName);
            var orderItems = await order.GetItems(CancellationToken.None);
            orderItems.Should().Contain(x => x.ProductId == productId && x.Amount == amount);
        }
    }
}
