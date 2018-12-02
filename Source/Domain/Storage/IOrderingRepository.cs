using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CheckoutChallenge.Domain.Model;

namespace CheckoutChallenge.Domain.Storage
{
    public interface IOrderingRepository
    {
        /// <summary>
        /// Gets all orders.
        /// Possible future enhancement would be adding search criteria and paging control.
        /// </summary>
        Task<IEnumerable<Order>> FindOrders(CancellationToken cancellationToken);

        Task<Order> GetOrder(Guid id, CancellationToken cancellationToken);

        Task StoreOrder(Order order, CancellationToken cancellationToken);
    }
}
