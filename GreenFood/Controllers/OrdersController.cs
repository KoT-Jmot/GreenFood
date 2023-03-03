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
        public async Task<IEnumerable<OutputOrderDto>> GetAllOrders()
        {
            return await _order.GetAllOrdersAsync();
        }

        [HttpGet("{orderId}")]
        public async Task<OutputOrderDto> GetOrderById(Guid orderId)
        {
            return await _order.GetOrderByIdAsync(orderId);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto inputOrderDto)
        {
            var userId = User.GetUserId();

            var orderId = await _order.CreateOrderByUserIdAsync(inputOrderDto, userId);

            return Ok(orderId);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrderById([FromRoute] Guid orderId)
        {
            var userId = User.GetUserId();

            await _order.DeleteOrderByIdAndUserIdAsync(userId, orderId);

            return NoContent();
        }
    }
}
