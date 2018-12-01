using System;
using System.Collections.Generic;

namespace CheckoutChallenge.Client
{
    public class Order
    {
        public Order(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
