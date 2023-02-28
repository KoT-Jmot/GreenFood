using GreenFood.Application.DTO;
using GreenFood.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using GreenFood.Domain.Utils;

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
            string userId = ClaimsConfiguration.GetUserId(User);

            await _product.CreateProductByUserId(productDto, userId);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromBody] ProductForDeleteDto productDto)
        {
            string userId = User.GetUserId();

            await _product.DeleteProductAsync(productDto, userId);

            return Ok();
        }
    }
}
