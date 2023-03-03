using GreenFood.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GreenFood.Domain.Utils;
using GreenFood.Application.DTO.InputDto;
using Mapster;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.Validation;
using FluentValidation;

namespace GreenFood.Web.Controllers
{
    [Route("Products")]
    public class ProductsController : Controller
    {
        private readonly IProductService _product;

        public ProductsController(IProductService product)
        {
            _product = product;
        }

        [HttpGet]
        public async Task<IEnumerable<OutputProductDto>> GetAllProducts()
        {
            return await _product.GetAllProductsAsync();
        }

        [HttpGet("{productId}")]
        public async Task<OutputProductDto> GetProductById([FromRoute] Guid productId)
        {
            return await _product.GetProductByIdAsync(productId);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            var userId = User.GetUserId();

            var productId = await _product.CreateProductByUserIdAsync(productDto, userId);

            return Ok(productId);
        }

        [Authorize]
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductById([FromRoute] Guid productId)
        {
            string userId = User.GetUserId();

            await _product.DeleteProductByIdAndUserIdAsync(userId, productId);

            return NoContent();
        }
    }
}
