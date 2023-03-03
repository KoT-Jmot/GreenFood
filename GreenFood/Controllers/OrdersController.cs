using FluentValidation;
using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.Validation;
using GreenFood.Domain.Utils;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Controllers
{
    [Route("Orders")]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _order;

        public OrdersController(IOrderService order)
        {
            _order = order;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            var orders = await _order.GetAllOrdersAsync();

            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _order.GetOrderByIdAsync(orderId);

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderDto inputOrderDto)
        {
            var userId = User.GetUserId();

            var orderId = await _order.CreateOrderByUserIdAsync(inputOrderDto, userId);

            return Created(nameof(CreateOrderAsync), orderId);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrderByIdAsync([FromRoute] Guid orderId)
        {
            var userId = User.GetUserId();

            await _order.DeleteOrderByIdAndUserIdAsync(userId, orderId);

            return NoContent();
        }
    }
}
