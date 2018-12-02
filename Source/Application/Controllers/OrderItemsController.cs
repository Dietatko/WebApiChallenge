using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CheckoutChallenge.Application.Translators;
using CheckoutChallenge.Domain.Model;
using CheckoutChallenge.Domain.Storage;
using Microsoft.AspNetCore.Mvc;
using Order = CheckoutChallenge.Domain.Model.Order;
using OrderItem = CheckoutChallenge.Domain.Model.OrderItem;

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
        public async Task<IActionResult> GetItems(Guid orderId, CancellationToken cancellationToken)
        {
            var order = await repository.GetOrder(orderId, cancellationToken);
            if (order == null)
                return NotFound();

            return Ok(order.Items.ToDto(x => CreateItemUrl(order, x)));
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

            return Ok(item.ToDto(CreateItemUrl(order, item)));
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

            var orderUrl = CreateItemUrl(order, item);
            return Created(orderUrl, item.ToDto(orderUrl));
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

            if (item.ProductId != itemDto.ProductId)
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

            return Ok(item.ToDto(CreateItemUrl(order, item)));
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

        [HttpDelete()]
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
