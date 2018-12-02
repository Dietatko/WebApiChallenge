using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheckoutChallenge.Client
{
    public class OrderItem
    {
        private readonly IInternalOrderingClient client;

        internal OrderItem(IInternalOrderingClient client, Uri id, Guid productId, decimal amount, DateTime createdAt, DateTime lastModifiedAt)
        {
            this.client = client;
            Id = id;
            ProductId = productId;
            Amount = amount;
            CreatedAt = createdAt;
            LastModifiedAt = lastModifiedAt;
        }

        public Uri Id { get; }
        public Guid ProductId { get; private set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastModifiedAt { get; private set; }

        public async Task Update(CancellationToken cancellationToken)
        {
            var updatedItem = await client.GetOrderItem(Id, cancellationToken);
            UpdateData(updatedItem);
        }

        public async Task Store(CancellationToken cancellationToken)
        {
            var updatedItem = await client.StoreOrderItem(this, cancellationToken);
            UpdateData(updatedItem);
        }

        public Task Delete(CancellationToken cancellationToken)
        {
            return client.DeleteOrderItem(this, cancellationToken);
        }

        private void UpdateData(OrderItem updatedItem)
        {
            ProductId = updatedItem.ProductId;
            Amount = updatedItem.Amount;
            CreatedAt = updatedItem.CreatedAt;
            LastModifiedAt = updatedItem.LastModifiedAt;
        }
    }
}
