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
    public class CategoryService : ICategoryService
    {
        private readonly AddCategoryValidator _addValidator;
        private readonly IRepositoryManager _manager;

        public CategoryService(
            AddCategoryValidator addValidator,
            IRepositoryManager manager)
        {
            _addValidator = addValidator;
            _manager = manager;
        }

        public async Task CreateCategoryAsync(CategoryForAddDto categoryDto)
        {
            await _addValidator.ValidateAndThrowAsync(categoryDto);

            if (_manager.Categories.GetAll().Where(t => t.Name == categoryDto.Name).Any())
                throw new CategoryException("This type already exists!");

            var category = categoryDto.Adapt<Category>();

            await _manager.Categories.AddAsync(category);
        }
    }
}
