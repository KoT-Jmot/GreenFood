using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GreenFood.Web.Controllers.AdminControllers
{
    [Authorize(Roles = AccountRoles.GetAdministratorRole)]
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
            [FromBody] UserIdForAdminDto usertDto,
            CancellationToken cancellationToken)
        {
            if(usertDto.UserId.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(usertDto));

            await _productManager.DeleteProductByIdAndUserIdAsync(usertDto.UserId!, productId, cancellationToken);

            return NoContent();
        }
    }
}
