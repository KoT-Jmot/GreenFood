using GreenFood.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GreenFood.Domain.Utils;
using GreenFood.Application.DTO.InputDto;

namespace GreenFood.Web.Controllers
{
    [Route("Products")]
    [Authorize]
    public class ProductsController : Controller
    {
       private readonly IProductService _product;

        public ProductsController(IProductService product)
        {
            _product = product;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductForAddDto productDto)
        {
            string userId = User.GetUserId();

            await _product.CreateProductByUserId(productDto, userId);

            return Ok();
        }

        [HttpDelete("/{productId}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid productId)
        {
            string userId = User.GetUserId();

            await _product.DeleteProductByIdAndUserIdAsync(userId, productId);

            return Ok();
        }
    }
}
