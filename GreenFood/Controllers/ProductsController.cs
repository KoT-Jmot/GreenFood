using GreenFood.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GreenFood.Domain.Utils;
using GreenFood.Application.DTO.InputDto;
using Mapster;
using GreenFood.Application.DTO.ServicesDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.Validation;
using FluentValidation;

namespace GreenFood.Web.Controllers
{
    [Route("Products")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService _product;
        private readonly AddProductValidation _addValidationRules;

        public ProductsController(
            IProductService product,
            AddProductValidation addValidationRules)
        {
            _product = product;
            _addValidationRules = addValidationRules;
        }

        [HttpGet("{userEmail}")]
        public async Task<IEnumerable<OutputProductDto>> GetProductByUserEmail([FromRoute] string userEmail)
        {
            return await _product.GetProductByUserEmail(userEmail);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] InputProductDto inputProductDto)
        {
            await _addValidationRules.ValidateAndThrowAsync(inputProductDto);

            var productDto = inputProductDto.Adapt<ProductDto>();
            productDto.SellerId = User.GetUserId();

            var productId = await _product.CreateProductByUserId(productDto);

            return Ok(productId);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductById([FromRoute] Guid productId)
        {
            string userId = User.GetUserId();

            await _product.DeleteProductByIdAndUserIdAsync(userId, productId);

            return Ok(StatusCode(200));
        }
    }
}
