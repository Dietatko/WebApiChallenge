using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CheckoutChallenge.Client
{
    internal interface IInternalOrderingClient
    {
        Task<Order> GetOrder(Uri id, CancellationToken cancellationToken);
        Task<Order> StoreOrder(Order order, CancellationToken cancellationToken);
        Task<IEnumerable<OrderItem>> GetOrderItems(Order order, CancellationToken cancellationToken);
        Task<OrderItem> GetOrderItem(Uri id, CancellationToken cancellationToken);
        Task<OrderItem> CreateOrderItem(Order order, Guid productId, decimal amount, CancellationToken cancellationToken);
        Task<OrderItem> StoreOrderItem(OrderItem item, CancellationToken cancellationToken);
        Task DeleteOrderItem(OrderItem item, CancellationToken cancellationToken);
        Task ClearOrder(Order order, CancellationToken cancellationToken);
        Task DeleteOrder(Order order, CancellationToken cancellationToken);
    }
}
