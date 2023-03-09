using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GreenFood.Web.Controllers.AdminControllers
{
    [Route("Admin/Products")]
    [Authorize(Roles = AccountRoles.GetAdministratorRole)]
    public class ProductsAdminController : Controller
    {
        private readonly IProductService _product;

        public ProductsAdminController(IProductService product)
        {
            _product = product;
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductByIdAsync(
        [FromRoute] Guid productId,
        [FromBody] UserIdForAdminDto usertDto,
        CancellationToken cancellationToken)
        {
            if(usertDto.UserId.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(usertDto.UserId));

            await _product.DeleteProductByIdAndUserIdAsync(usertDto.UserId!, productId, cancellationToken);

            return NoContent();
        }
    }
}
