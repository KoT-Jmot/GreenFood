using GreenFood.Application.DTO.InputDto.ProductDto;
using GreenFood.Application.Services;
using GreenFood.Application.Validation;
using GreenFood.Infrastructure.Configurations;
using Moq;

namespace GreenFood.Tests.ServiceTests
{
    public class ProductServciceTests
    {
        private readonly Mock<IRepositoryManager> _repositoryManager = new();
        //private readonly Mock<ProductValidation> _productValidator = new();

        [Fact]
        public async Task CreateProductBySallerIdAsync_RightProductData_GoodResponse()
        {
            var _productValidator = new ProductValidation();

            var productService = new ProductService(_repositoryManager.Object, _productValidator);

            var productDto = new ProductDto
            {
                CategoryId = Guid.NewGuid(),
                Count = 300,
                Description = "Test Description",
                Header = "Test",
                Price = 15
            };
            var sellerId = Guid.NewGuid().ToString();

            await productService.CreateProductBySallerIdAsync(productDto, sellerId, default);
        }
    }
}
