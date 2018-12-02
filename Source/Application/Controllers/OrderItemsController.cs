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
    [Route("v1/orders/{orderId}/items")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderingRepository repository;

        public OrderItemsController(IOrderingRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderItems(Guid orderId, CancellationToken cancellationToken)
        {
            var order = await repository.GetOrder(orderId, cancellationToken);
            if (order == null)
                return NotFound();

            return Ok(new DataContracts.OrderItem[0]);
        }
        /*
        [HttpGet("{itemId}", Name = nameof(GetOrderItem))]
        public async Task<IActionResult> GetOrderItem(Guid orderId, Guid itemId, CancellationToken cancellationToken)
        {
            var order = await repository.GetOrder(id, cancellationToken);
            if (order == null)
                return NotFound();

            return Ok(order.ToDto(CreateOrderItemUrl(order)));
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

        private Uri CreateOrderItemUrl(Order order)
        {
            return new Uri(
                Url.Action(
                    nameof(GetOrderItem), 
                    new
                    {
                        orderId = order.Id,
                        itemId = order.Id
                    }), 
                UriKind.Relative);
        }*/
    }
}
