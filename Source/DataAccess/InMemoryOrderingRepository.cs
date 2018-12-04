using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CheckoutChallenge.Domain.Model;

namespace CheckoutChallenge.Domain.Storage
{
    public class InMemoryOrderingRepository : IOrderingRepository
    {
        private readonly ConcurrentDictionary<Guid, Order> orderStore = new ConcurrentDictionary<Guid, Order>();

        public Task<IEnumerable<Order>> FindOrders(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => 
                (IEnumerable<Order>)orderStore
                    .Values
                    .Where(x => !x.IsDeleted)
                    .ToList(), 
                cancellationToken);
        }

        public Task<Order> GetOrder(Guid id, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                orderStore.TryGetValue(id, out var order);

                if (order != null && order.IsDeleted)
                    order = null;

                return order;
            }, cancellationToken);
        }

        public Task StoreOrder(Order order, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                orderStore.AddOrUpdate(
                    order.Id, 
                    order, 
                    (id, existing) => order);
            }, cancellationToken);
        }
    }
}