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
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var products = await _product.GetAllProductsAsync();

            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductByIdAsync([FromRoute] Guid productId)
        {
            var product = await _product.GetProductByIdAsync(productId);

            return Ok(product);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductDto productDto)
        {
            var userId = User.GetUserId();

            var productId = await _product.CreateProductByUserIdAsync(productDto, userId);

            return Created(nameof(CreateProductAsync), productId);
        }

        [Authorize]
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductByIdAsync([FromRoute] Guid productId)
        {
            var userId = User.GetUserId();

            await _product.DeleteProductByIdAndUserIdAsync(userId, productId);

            return NoContent();
        }
    }
}
