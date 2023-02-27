using GreenFood.Application.DTO;
using GreenFood.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Controllers
{
    [Route("Product")]
    [Authorize]
    public class ProductController : Controller
    {
       private readonly IProductService _product;

        public ProductController(IProductService product)
        {
            _product = product;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddProduct([FromBody] ProductForAddDto productDto)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.NameIdentifier)!.Value;

            productDto.SellerId = userId;

            await _product.CreateProduct(productDto);

            return Ok();
        }
    }
}
