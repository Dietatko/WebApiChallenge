using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CheckoutChallenge.Application.Translators;
using CheckoutChallenge.Domain.Model;
using CheckoutChallenge.Domain.Storage;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutChallenge.Application.Controllers
{
    [Controller]
    [Produces("application/json")]
    [Route("v1/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderingRepository repository;

        public OrdersController(IOrderingRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
        {
            var orders = await repository.FindOrders(cancellationToken)
                         ?? new Order[0];

            return Ok(new DataContracts.OrderList {
                Items = orders.ToDto()
            });
        }
    }
}
