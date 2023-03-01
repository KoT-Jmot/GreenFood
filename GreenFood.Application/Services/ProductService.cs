using FluentValidation;
using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.Validation;
using GreenFood.Domain.Exceptions;
using GreenFood.Domain.Models;
using GreenFood.Infrastructure.Configurations;
using Mapster;

namespace GreenFood.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryManager _manager;
        private readonly AddProductValidation _addValidationRules;

        public ProductService(
            IRepositoryManager manager,
            AddProductValidation addValidationRules)
        {
            _manager = manager;
            _addValidationRules = addValidationRules;
        }

        public async Task CreateProductByUserId(ProductForAddDto productDto, string userId)
        {
            await _addValidationRules.ValidateAndThrowAsync(productDto);

            if (!_manager.Categories.CategoryByIdExisted(productDto.CategoryId))
                throw new CategoryException();

            var product = productDto.Adapt<Product>();
            product.SellerId = userId;

            await _manager.Products.AddAsync(product);

            await _manager.SaveChangesAsync();
        }

        public async Task DeleteProductByIdAndUserIdAsync(string userId, Guid productId)
        {
            var product = _manager.Products.GetProductByIdAndUserId(productId, userId);

            if (product is null)
                throw new Exception();

            await _manager.Products.RemoveAsync(product);

            await _manager.SaveChangesAsync();
        }
    }
}
