using System;
using System.Collections.Generic;

namespace CheckoutChallenge.Domain.Model
{
    public class Order
    {
        private int itemId = 0;
        private readonly List<OrderItem> items = new List<OrderItem>();

        public Order(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
            CreatedAt = DateTime.UtcNow;
            LastModifiedAt = CreatedAt;
        }

        public Guid Id { get; }

        public Guid CustomerId
        {
            get => customerId;
            set
            {
                if (value == Guid.Empty)
                    throw new DataValidationException("A valid customer id has to be specified.");

                customerId = value;
                LastModifiedAt = DateTime.UtcNow;
            }
        }
        private Guid customerId;

        public IEnumerable<OrderItem> Items => items.ToArray();

        public DateTime CreatedAt { get; }

        public DateTime LastModifiedAt { get; private set; }

        public OrderItem AddItem(Guid productId, decimal amount)
        {
            var item = new OrderItem(++itemId, productId, amount);
            items.Add(item);

            LastModifiedAt = DateTime.UtcNow;

            return item;
        }

        public void DeleteItem(OrderItem item)
        {
            if (!items.Contains(item))
                throw new DataValidationException($"Item {item.Id} is not in the order '{Id}'.");

            items.Remove(item);

            LastModifiedAt = DateTime.UtcNow;
        }
    }
}
