using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.RequestFeatures;
using GreenFood.Web.features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Controllers.UserControllers
{
    [Authorize(Policy = "IsNotBlocked")]
    [Route("Orders")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _order;

        public OrdersController(IOrderService order)
        {
            _order = order;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrdersAsync(
            [FromQuery] OrderQueryDto orderQuery,
            CancellationToken cancellationToken)
        {
            var orders = await _order.GetAllOrdersAsync(orderQuery, cancellationToken);

            return new PagingActionResult<PagedList<OutputOrderDto>>(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderByIdAsync(
            Guid orderId,
            CancellationToken cancellationToken)
        {
            var order = await _order.GetOrderByIdAsync(orderId, cancellationToken);

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(
            [FromBody] OrderDto inputOrderDto,
            CancellationToken cancellationToken)
        {
            var customerId = User.GetUserId();

            var orderId = await _order.CreateOrderByCustomerIdAsync(inputOrderDto, customerId, cancellationToken);

            return Created(nameof(CreateOrderAsync), orderId);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrderByIdAsync(
            [FromRoute] Guid orderId,
            CancellationToken cancellationToken)
        {
            var customerId = User.GetUserId();

            await _order.DeleteOrderByIdAndCustomerIdAsync(customerId, orderId, cancellationToken);

            return NoContent();
        }
    }
}
