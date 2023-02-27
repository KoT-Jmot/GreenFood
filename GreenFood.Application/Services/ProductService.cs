using FluentValidation;
using GreenFood.Application.Contracts;
using GreenFood.Application.DTO;
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
        private readonly AddProductValidation _validationRules;

        public ProductService(
            IRepositoryManager manager,
            AddProductValidation validationRules)
        {
            _manager = manager;
            _validationRules = validationRules;
        }

        public async Task CreateProduct(ProductForAddDto productDto)
        {
            await _validationRules.ValidateAndThrowAsync(productDto);

            if (!_manager.Categories.GetAll().Where(t => t.Id == productDto.CategoryId).Any())
                throw new CategoryException();

            var product = productDto.Adapt<Product>();

            await _manager.Products.AddAsync(product);
        }

    }
}
