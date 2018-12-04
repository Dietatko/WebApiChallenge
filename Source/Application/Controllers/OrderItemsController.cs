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
using Order = CheckoutChallenge.Domain.Model.Order;
using OrderItem = CheckoutChallenge.Domain.Model.OrderItem;

namespace CheckoutChallenge.Application.Controllers
{
    [Controller]
    [Route("v1/orders/{orderId}/items")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderingRepository repository;

        public OrderItemsController(IOrderingRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet(Name = nameof(GetItems))]
        public async Task<IActionResult> GetItems(Guid orderId, CancellationToken cancellationToken)
        {
            var order = await repository.GetOrder(orderId, cancellationToken);
            if (order == null)
                return NotFound();

            return Ok(GetHalResponse(order));
        }
        
        [HttpGet("{itemId}", Name = nameof(GetItem))]
        public async Task<IActionResult> GetItem(Guid orderId, int itemId, CancellationToken cancellationToken)
        {
            var order = await repository.GetOrder(orderId, cancellationToken);
            if (order == null)
                return NotFound();

            var item = order.Items.SingleOrDefault(x => x.Id == itemId);
            if (item == null)
                return NotFound();

            return Ok(GetHalResponse(order, item));
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(Guid orderId, [FromBody] DataContracts.OrderItem itemDto, CancellationToken cancellationToken)
        {
            var order = await repository.GetOrder(orderId, cancellationToken);
            if (order == null)
                return NotFound();

            OrderItem item;
            try
            {
                item = order.Items.SingleOrDefault(x => x.ProductId == itemDto.ProductId);
                if (item != null)
                {
                    item.Amount += itemDto.Amount;
                }
                else
                {
                    item = order.AddItem(itemDto.ProductId, itemDto.Amount);
                }
            }
            catch (DataValidationException ex)
            {
                return BadRequest(new DataContracts.Error(ex.Message));
            }
            
            await repository.StoreOrder(order, cancellationToken);

            return Created(
                CreateItemUrl(order, item), 
                GetHalResponse(order, item));
        }

        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateItem(Guid orderId, int itemId, [FromBody] DataContracts.OrderItem itemDto, CancellationToken cancellationToken)
        {
            var order = await repository.GetOrder(orderId, cancellationToken);
            if (order == null)
                return NotFound();

            var item = order.Items.SingleOrDefault(x => x.Id == itemId);
            if (item == null)
                return NotFound();

            if (itemDto.ProductId != default && item.ProductId != itemDto.ProductId)
                return BadRequest(new DataContracts.Error("Product cannot be changed."));

            try
            {
                item.Amount = itemDto.Amount;
            }
            catch (DataValidationException ex)
            {
                return BadRequest(new DataContracts.Error(ex.Message));
            }

            await repository.StoreOrder(order, cancellationToken);

            return Ok(GetHalResponse(order, item));
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteItem(Guid orderId, int itemId, CancellationToken cancellationToken)
        {
            var order = await repository.GetOrder(orderId, cancellationToken);
            if (order == null)
                return NotFound();

            var item = order.Items.SingleOrDefault(x => x.Id == itemId);
            if (item == null)
                return NotFound();

            try
            {
                order.DeleteItem(item);
            }
            catch (DataValidationException ex)
            {
                return BadRequest(new DataContracts.Error(ex.Message));
            }

            await repository.StoreOrder(order, cancellationToken);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> ClearItems(Guid orderId, CancellationToken cancellationToken)
        {
            var order = await repository.GetOrder(orderId, cancellationToken);
            if (order == null)
                return NotFound();

            try
            {
                order.Clear();
            }
            catch (DataValidationException ex)
            {
                return BadRequest(new DataContracts.Error(ex.Message));
            }

            await repository.StoreOrder(order, cancellationToken);

            return NoContent();
        }

        private HALResponse GetHalResponse(Order order)
        {
            var items = order.Items.ToArray();
            return new HALResponse(new {})
                .AddSelfLink(CreateItemsUrl(order))
                .AddEmbeddedCollection(HalUtils.ItemRel, items.Select(x => GetHalResponse(order, x)));
        }

        private HALResponse GetHalResponse(Order order, OrderItem item)
        {
            var itemUrl = CreateItemUrl(order, item);

            return new HALResponse(item.ToDto(itemUrl))
                .AddSelfLink(itemUrl)
                .AddCheckoutLink(DataContracts.Relations.Order, CreateOrderUrl(order))
                .AddCheckoutLink(DataContracts.Relations.OrderItems, CreateItemsUrl(order));
        }

        private Uri CreateOrderUrl(Order order)
        {
            return new Uri(Url.Action(nameof(OrdersController.GetOrder), "Orders", new { id = order.Id }), UriKind.Relative);
        }

        private Uri CreateItemsUrl(Order order)
        {
            return new Uri(Url.Action(nameof(GetItems), new { orderId = order.Id, }), UriKind.Relative);
        }

        private Uri CreateItemUrl(Order order, OrderItem item)
        {
            return new Uri(
                Url.Action(
                    nameof(GetItem), 
                    new
                    {
                        orderId = order.Id,
                        itemId = item.Id
                    }), 
                UriKind.Relative);
        }
    }
}
