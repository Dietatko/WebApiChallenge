using System;
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

        [HttpGet("{id}", Name = nameof(GetOrder))]
        public async Task<IActionResult> GetOrder(Guid id, CancellationToken cancellationToken)
        {
            var order = await repository.GetOrder(id, cancellationToken);
            if (order == null)
                return NotFound();

            return Ok(order.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] DataContracts.Order orderDto, CancellationToken cancellationToken)
        {
            Order newOrder;
            try
            {
                newOrder = new Order(Guid.NewGuid(), orderDto.CustomerId);
            }
            catch (DataValidationException ex)
            {
                return BadRequest(new DataContracts.Error(ex.Message));
            }
            
            await repository.StoreOrder(newOrder, cancellationToken);
            return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder.ToDto());
        }
    }
}
