using System;
using System.Collections.Generic;

namespace CheckoutChallenge.Domain.Model
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
