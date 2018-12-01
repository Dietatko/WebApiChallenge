using System;
using System.Collections.Generic;

namespace CheckoutChallenge.DataContracts
{
    public class OrderList
    {
        public IEnumerable<Order> Items { get; set; }
    }
}
