using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GreenFood.Web.Controllers.AdminControllers
{
    [Route("Admin/Orders")]
    [Authorize(Roles = AccountRoles.GetAdministratorRole)]
    public class OrdersAdminController : Controller
    {
        private readonly IOrderService _order;

        public OrdersAdminController(IOrderService order)
        {
            _order = order;
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrderByIdAsync(
            [FromRoute] Guid orderId,
            [FromBody] UserIdForAdminDto userDto,
            CancellationToken cancellationToken)
        {
            if (userDto.UserId.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(userDto.UserId));

            await _order.DeleteOrderByIdAndUserIdAsync(userDto.UserId!, orderId, cancellationToken);

            return NoContent();
        }
    }
}
