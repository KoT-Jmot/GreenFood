using FluentValidation;
using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.DTO.ServicesDto;
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
        private readonly AddOrderValidator _addValidationRules;

        public OrdersController(
            IOrderService order,
            AddOrderValidator addValidationRules)
        {
            _order = order;
            _addValidationRules = addValidationRules;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] InputOrderDto inputOrderDto)
        {
            await _addValidationRules.ValidateAndThrowAsync(inputOrderDto);

            var orderDto = inputOrderDto.Adapt<OrderDto>();
            orderDto.CustomerId = User.GetUserId();

            var orderId = await _order.CreateOrder(orderDto);

            return Ok(orderId);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrderById([FromRoute] Guid orderId)
        {
            string userId = User.GetUserId();

            await _order.DeleteOrderByIdAndUserId(userId, orderId);

            return Ok(StatusCode(200));
        }

        [HttpGet]
        public async Task<IEnumerable<OutputOrderDto>> GetUsersOrders()
        {
            string userId = User.GetUserId();

            return await _order.GetOrdersByUserId(userId);
        }
    }
}
