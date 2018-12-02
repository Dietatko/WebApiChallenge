using System;
using System.Threading;
using System.Threading.Tasks;

namespace CheckoutChallenge.Client
{
    internal interface IInternalOrderingClient
    {
        Task<Order> GetOrder(Uri id, CancellationToken cancellationToken);

        Task<Order> StoreOrder(Order order, CancellationToken cancellationToken);
    }
}
