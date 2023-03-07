using GreenFood.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GreenFood.Domain.Utils;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Web.features;
using GreenFood.Application.RequestFeatures;

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
        public async Task<IActionResult> GetAllProductsAsync(
            [FromQuery] ProductQueryDto productQuery,
            CancellationToken cancellationToken)
        {
            var products = await _product.GetAllProductsAsync(productQuery, cancellationToken);

            return new CustomActionResult<PagedList<OutputProductDto>>(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductByIdAsync(
            [FromRoute] Guid productId,
            CancellationToken cancellationToken)
        {
            var product = await _product.GetProductByIdAsync(productId, cancellationToken);

            return Ok(product);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(
            [FromBody] ProductDto productDto,
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var productId = await _product.CreateProductByUserIdAsync(productDto, userId, cancellationToken);

            return Created(nameof(CreateProductAsync), productId);
        }

        [Authorize]
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductByIdAsync(
            [FromRoute] Guid productId,
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            await _product.DeleteProductByIdAndUserIdAsync(userId, productId, cancellationToken);

            return NoContent();
        }
    }
}
