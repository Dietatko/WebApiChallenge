using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CheckoutChallenge.Client
{
    public class Order
    {
        private readonly IInternalOrderingClient client;

        internal Order(IInternalOrderingClient client, Uri id, Guid customerId, DateTime createdAt, DateTime lastModifiedAt)
        {
            this.client = client;
            Id = id;
            CustomerId = customerId;
            CreatedAt = createdAt;
            LastModifiedAt = lastModifiedAt;
        }

        public Uri Id { get; }
        public Guid CustomerId { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastModifiedAt { get; private set; }

        public async Task Update(CancellationToken cancellationToken)
        {
            var updatedOrder = await client.GetOrder(Id, cancellationToken);
            UpdateData(updatedOrder);
        }

        public async Task Store(CancellationToken cancellationToken)
        {
            var updatedOrder = await client.StoreOrder(this, cancellationToken);
            UpdateData(updatedOrder);
        }

        public Task<IEnumerable<OrderItem>> GetItems(CancellationToken cancellationToken)
        {
            return client.GetOrderItems(this, cancellationToken);
        }

        private void UpdateData(Order updatedOrder)
        {
            CustomerId = updatedOrder.CustomerId;
            CreatedAt = updatedOrder.CreatedAt;
            LastModifiedAt = updatedOrder.LastModifiedAt;
        }
    }
}
