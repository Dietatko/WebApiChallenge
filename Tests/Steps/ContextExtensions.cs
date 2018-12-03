using System;
using CheckoutChallenge.Client;
using TechTalk.SpecFlow;

namespace CheckoutChallenge.AcceptanceTests.Steps
{
    public static class ContextExtensions
    {
        public static void StoreOrder(this ScenarioContext context, string name, Order order)
        {
            context.Set(order, GetOrderKey(name));
        }

        public static Order GetOrder(this ScenarioContext context, string name)
        {
            return context.Get<Order>(GetOrderKey(name));
        }

        private static string GetOrderKey(string name) => $"{nameof(Order)}_{name}";
    }
}
