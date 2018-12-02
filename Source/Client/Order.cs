using System;

namespace CheckoutChallenge.Client
{
    public class Order
    {
        public Order(Guid id, Guid customerId, DateTime createdAt, DateTime lastModifiedAt)
        {
            Id = id;
            CustomerId = customerId;
            CreatedAt = createdAt;
            LastModifiedAt = lastModifiedAt;
        }

        public Guid Id { get; }
        public Guid CustomerId { get; set; }
        public DateTime CreatedAt { get; }
        public DateTime LastModifiedAt { get; }
    }
}
