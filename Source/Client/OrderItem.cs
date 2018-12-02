using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutChallenge.Client
{
    public class OrderItem
    {
        private readonly OrderingClient client;

        public OrderItem(OrderingClient client, Uri id, Guid productId, decimal amount, DateTime createdAt, DateTime lastModifiedAt)
        {
            this.client = client;
            Id = id;
            ProductId = productId;
            Amount = amount;
            CreatedAt = createdAt;
            LastModifiedAt = lastModifiedAt;
        }

        public Uri Id { get; }
        public Guid ProductId { get; }
        public decimal Amount { get; }
        public DateTime CreatedAt { get; }
        public DateTime LastModifiedAt { get; }
    }
}
