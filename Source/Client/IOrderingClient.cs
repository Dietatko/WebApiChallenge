using System;
using System.Threading;
using System.Threading.Tasks;

namespace CheckoutChallenge.Client
{
    public interface IOrderingClient
    {
        Task<OrderList> GetOrders(CancellationToken cancellationToken);

        Task<Order> GetOrder(Guid id, CancellationToken cancellationToken);

        Task<Order> CreateOrder(Guid customerId, CancellationToken cancellationToken);
    }
}
