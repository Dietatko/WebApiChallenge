using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CheckoutChallenge.Client
{
    public interface IOrderingClient
    {
        Task<OrderList> GetOrders(CancellationToken cancellationToken);
    }
}
