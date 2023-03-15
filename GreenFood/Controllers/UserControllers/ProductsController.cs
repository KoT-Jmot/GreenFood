using GreenFood.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Web.features;
using GreenFood.Application.RequestFeatures;

namespace GreenFood.Web.Controllers.UserControllers
{
    [Route("Products")]
    public class ProductsController : Controller
    {
        private readonly IProductService _product;

        public ProductsController(IProductService product)
        {
            _product = product;
        }

        public async Task<IActionResult> GetAllProductsAsync(
            [FromQuery] ProductQueryDto productQuery,
            CancellationToken cancellationToken)
        {
            var products = await _product.GetAllProductsAsync(productQuery, cancellationToken);

            return new PagingActionResult<PagedList<OutputProductDto>>(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductByIdAsync(
            [FromRoute] Guid productId,
            CancellationToken cancellationToken)
        {
            var product = await _product.GetProductByIdAsync(productId, cancellationToken);

            return Ok(product);
        }

        [Authorize(Policy = "IsNotBlocked")]
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(
            [FromBody] ProductDto productDto,
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var productId = await _product.CreateProductBySallerIdAsync(productDto, userId, cancellationToken);

            return Created(nameof(CreateProductAsync), productId);
        }

        [Authorize(Policy = "IsNotBlocked")]
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductByIdAsync(
            [FromRoute] Guid productId,
            CancellationToken cancellationToken)
        {
            var sallerId = User.GetUserId();

            await _product.DeleteProductByIdAndSallerIdAsync(sallerId, productId, cancellationToken);

            return NoContent();
        }

        [Authorize(Policy = "IsNotBlocked")]
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProductAsync(
            [FromRoute] Guid productId,
            [FromBody] ProductDto productDto,
            CancellationToken cancellationToken)
        {
            var sallerId = User.GetUserId();

            var updatedProductId = await _product.UpdateProductByIdAndSallerIdAsync(productId, productDto, sallerId, cancellationToken);

            return Ok(updatedProductId);
        }
    }
}
