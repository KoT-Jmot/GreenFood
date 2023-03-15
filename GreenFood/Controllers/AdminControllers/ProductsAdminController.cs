using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GreenFood.Web.Controllers.AdminControllers
{
    [Authorize(Policy = "IsNotBlocked", Roles = AccountRoles.GetAdministratorRole)]
    [Route("Admin/Products")]
    public class ProductsAdminController : Controller
    {
        private readonly IProductService _productManager;
        
        public ProductsAdminController(IProductService productManager)
        {
            _productManager = productManager;
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductByIdAsync(
            [FromRoute] Guid productId,
            [FromBody] UserIdForAdminDto userDto,
            CancellationToken cancellationToken)
        {
            if(userDto.UserId.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(userDto));

            await _productManager.DeleteProductByIdAndSallerIdAsync(userDto.UserId!, productId, cancellationToken);

            return NoContent();
        }
    }
}
