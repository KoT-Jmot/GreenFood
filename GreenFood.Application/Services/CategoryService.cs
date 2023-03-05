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
        private readonly AddCategoryValidator _CategoryValidator;

        public CategoryService(
            IRepositoryManager manager,
            AddCategoryValidator CategoryValidator)
        {

            _manager = manager;
            _CategoryValidator = CategoryValidator;
        }

        public async Task<OutputCategoryDto> GetCategoryByIdAsync(
            Guid categoryId,
            CancellationToken cancellationToken)
        {
            var category = await _manager.Categories.GetByIdAsync(categoryId, false, cancellationToken);

            if (category is null)
                throw new EntityNotFoundException("Category was not found!");

            var outputCategory = category.Adapt<OutputCategoryDto>();

            return outputCategory;
        }

        public async Task<IEnumerable<OutputCategoryDto>> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            var categories = await _manager.Categories.GetAll().ToListAsync(cancellationToken);

            var outputCategories = categories.Adapt<IEnumerable<OutputCategoryDto>>();

            return outputCategories;
        }
        
        public async Task<Guid> CreateCategoryAsync(
            CategoryDto categoryDto,
            CancellationToken cancellationToken)
        {

            await _CategoryValidator.ValidateAndThrowAsync(categoryDto, cancellationToken);

            if (await _manager.Categories.GetCategoryByNameAsync(categoryDto.Name!,false, cancellationToken) is not null)
                throw new CreatingCategoryException("This category already exists!");

            var category = categoryDto.Adapt<Category>();

            await _manager.Categories.AddAsync(category, cancellationToken);
            await _manager.SaveChangesAsync(cancellationToken);

            return category.Id;
        }

        public async Task DeleteCategoryByIdAsync(
            Guid categoryId,
            CancellationToken cancellationToken)
        {
            var category = await _manager.Categories.GetByIdAsync(categoryId,false, cancellationToken);

            if (category is null)
                throw new EntityNotFoundException("Category was not found!");

            await _manager.Categories.RemoveAsync(category, cancellationToken);
            await _manager.SaveChangesAsync(cancellationToken);
        }
    }
}
