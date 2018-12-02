using System;
using System.Threading;
using System.Threading.Tasks;

namespace CheckoutChallenge.Client
{
    public interface IOrderingClient
    {
        Task<Order> CreateOrder(Guid customerId, CancellationToken cancellationToken);
        Task<OrderList> FindOrders(CancellationToken cancellationToken);
    }
}
