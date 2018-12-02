using System;
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
                Items = orders.ToDto(CreateOrderUrl)
            });
        }

        [HttpGet("{id}", Name = nameof(GetOrder))]
        public async Task<IActionResult> GetOrder(Guid id, CancellationToken cancellationToken)
        {
            var order = await repository.GetOrder(id, cancellationToken);
            if (order == null)
                return NotFound();

            return Ok(order.ToDto(CreateOrderUrl(order)));
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

            var newOrderUrl = CreateOrderUrl(newOrder);
            return Created(newOrderUrl, newOrder.ToDto(newOrderUrl));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] DataContracts.Order orderDto, CancellationToken cancellationToken)
        {
            var order = await repository.GetOrder(id, cancellationToken);
            if (order == null)
                return NotFound();

            try
            {
                order.CustomerId = orderDto.CustomerId;
            }
            catch (DataValidationException ex)
            {
                return BadRequest(new DataContracts.Error(ex.Message));
            }

            await repository.StoreOrder(order, cancellationToken);

            return Ok(order.ToDto(CreateOrderUrl(order)));
        }

        private Uri CreateOrderUrl(Order order)
        {
            return new Uri(Url.Action(nameof(GetOrder), new {id = order.Id}), UriKind.Relative);
        }
    }
}
