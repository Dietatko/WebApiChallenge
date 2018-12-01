using System.Collections;
using System.Collections.Generic;

namespace CheckoutChallenge.Client
{
    public class OrderList : IEnumerable<Order>
    {
        private readonly IEnumerable<Order> orders;

        public OrderList(IEnumerable<Order> orders)
        {
            this.orders = orders;
        }

        // Place for list metadata like total number of orders, page number, page count, etc.

        public IEnumerator<Order> GetEnumerator()
        {
            return orders.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
