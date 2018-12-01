using CheckoutChallenge.DataContracts;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutChallenge.Application.Controllers
{
    [Controller]
    [Produces("application/json")]
    [Route("v1/orders")]
    public class OrdersController : ControllerBase
    {
        public OrdersController()
        {
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(new OrderList()
            {
                Items = new Order[0]
            });
        }
    }
}
