using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CheckoutChallenge.Application.Translators;
using CheckoutChallenge.Domain.Model;
using CheckoutChallenge.Domain.Storage;
using Halcyon.HAL;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutChallenge.Application.Controllers
{
    [Controller]
    [Route("v1/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderingRepository repository;

        public OrdersController(IOrderingRepository repository)
        {
            this.repository = repository;
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
            return Created(
                newOrderUrl,
                GetHalResponse(newOrder));
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
        {
            var orders = await repository.FindOrders(cancellationToken)
                         ?? new Order[0];

            return Ok(GetHalResponse(orders));
        }

        [HttpGet("{id}", Name = nameof(GetOrder))]
        public async Task<IActionResult> GetOrder(Guid id, CancellationToken cancellationToken)
        {
            var order = await repository.GetOrder(id, cancellationToken);
            if (order == null)
                return NotFound();

            return Ok(GetHalResponse(order));
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

            return Ok(GetHalResponse(order));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id, CancellationToken cancellationToken)
        {
            var order = await repository.GetOrder(id, cancellationToken);
            if (order == null)
                return NotFound();

            try
            {
                order.Delete();
            }
            catch (DataValidationException ex)
            {
                return BadRequest(new DataContracts.Error(ex.Message));
            }

            await repository.StoreOrder(order, cancellationToken);

            return NoContent();
        }

        private Uri CreateOrderUrl(Order order)
        {
            return new Uri(Url.Action(nameof(GetOrder), new {id = order.Id}), UriKind.Relative);
        }

        private Uri CreateOrderItemsUrl(Order order)
        {
            return new Uri(Url.Action(nameof(OrderItemsController.GetItems), "OrderItems", new { orderId = order.Id }), UriKind.Relative);
        }

        private HALResponse GetHalResponse(IEnumerable<Order> orders)
        {
            orders = orders.ToList();

            var dto = new DataContracts.OrderList
            {
                Items = orders.ToDto(CreateOrderUrl)
            };
            return new HALResponse(dto)
                .AddSelfLink(new Uri(Url.Action(nameof(GetOrders)), UriKind.Relative))
                .AddEmbeddedCollection(HalUtils.ItemRel, orders.Select(GetHalResponse));
        }

        private HALResponse GetHalResponse(Order order)
        {
            var orderUrl = CreateOrderUrl(order);
            var itemsUrl = CreateOrderItemsUrl(order);

            return new HALResponse(order.ToDto(orderUrl))
                .AddSelfLink(orderUrl)
                .AddCheckoutLink(DataContracts.Relations.OrderItems, itemsUrl);
        }
    }
}
