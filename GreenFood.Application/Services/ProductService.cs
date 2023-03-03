using FluentValidation;
using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.Validation;
using GreenFood.Domain.Exceptions;
using GreenFood.Domain.Models;
using GreenFood.Infrastructure.Configurations;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GreenFood.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryManager _manager;
        private readonly AddProductValidation _addProductValidator;

        public ProductService(
            IRepositoryManager manager,
            AddProductValidation addProductValidator)
        {
            _manager = manager;
            _addProductValidator = addProductValidator;
        }

        public async Task<OutputProductDto> GetProductByIdAsync(Guid productId)
        {
            var product = await _manager.Products.GetByIdAsync(productId);

            if (product is null)
                throw new EntityNotFoundException("Product was not found!");

            var outputProduct = product.Adapt<OutputProductDto>();

            return outputProduct;
        }

        public async Task<IEnumerable<OutputProductDto>> GetAllProductsAsync()
        {
            var products = await _manager.Products.GetAll().ToListAsync();

            var outputProducts = products.Adapt<IEnumerable<OutputProductDto>>();

            return outputProducts;
        }

        public async Task<Guid> CreateProductByUserIdAsync(
            ProductDto productDto,
            string sellerId)
        {
            await _addProductValidator.ValidateAndThrowAsync(productDto);

            var category = await _manager.Categories.GetByIdAsync(productDto.CategoryId);

            if (category is null)
                throw new EntityNotFoundException("Category was not found!");

            var product = productDto.Adapt<Product>();
            product.SellerId = sellerId;

            await _manager.Products.AddAsync(product);
            await _manager.SaveChangesAsync();

            return product.Id;
        }

        public async Task DeleteProductByIdAndUserIdAsync(
            string userId,
            Guid productId)
        {
            var product = await _manager.Products.GetProductByIdAndUserIdAsync(productId, userId);

            if (product is null)
                throw new EntityNotFoundException("Product was not found!");

            await _manager.Products.RemoveAsync(product);
            await _manager.SaveChangesAsync();
        }
    }
}
