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
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _manager;
        private readonly AddCategoryValidator _addRule;

        public CategoryService(
            IRepositoryManager manager,
            AddCategoryValidator addRule)
        {

            _manager = manager;
            _addRule = addRule;
        }

        public async Task<OutputCategoryDto> GetCategoryByIdAsync(Guid categoryId)
        {
            var category = await _manager.Categories.GetByIdAsync(categoryId);

            if (category is null)
                throw new EntityNotFoundException("Category was not found!");

            return category.Adapt<OutputCategoryDto>();
        }

        public async Task<IEnumerable<OutputCategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _manager.Categories.GetAll().ToListAsync();

            return categories.Adapt<IEnumerable<OutputCategoryDto>>();
        }
        
        public async Task<Guid> CreateCategoryAsync(CategoryDto categoryDto)
        {

            await _addRule.ValidateAndThrowAsync(categoryDto);

            if (await _manager.Categories.GetCategoryByNameAsync(categoryDto.Name) is not null)
                throw new CreatingCategoryException("This category already exists!");

            var category = categoryDto.Adapt<Category>();

            await _manager.Categories.AddAsync(category);

            await _manager.SaveChangesAsync();

            return category.Id;
        }

        public async Task DeleteCategoryByIdAsync(Guid categoryId)
        {
            var category = await _manager.Categories.GetByIdAsync(categoryId);

            if (category is null)
                throw new EntityNotFoundException("Category was not found!");

            await _manager.Categories.RemoveAsync(category);

            await _manager.SaveChangesAsync();
        }
    }
}
