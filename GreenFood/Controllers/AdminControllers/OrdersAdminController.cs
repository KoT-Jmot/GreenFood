using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GreenFood.Web.Controllers.AdminControllers
{
    [Authorize(Roles = AccountRoles.GetAdministratorRole)]
    [Route("Admin/Orders")]
    public class OrdersAdminController : Controller
    {
        private readonly IOrderService _orderManager;

        public OrdersAdminController(IOrderService orderManager)
        {
            _orderManager = orderManager;
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrderByIdAsync(
            [FromRoute] Guid orderId,
            [FromBody] UserIdForAdminDto userDto,
            CancellationToken cancellationToken)
        {
            if (userDto.UserId.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(userDto));

            await _orderManager.DeleteOrderByIdAndUserIdAsync(userDto.UserId!, orderId, cancellationToken);

            return NoContent();
        }
    }
}
