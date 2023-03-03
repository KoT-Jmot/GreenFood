using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Domain.Exceptions;
using GreenFood.Domain.Models;
using GreenFood.Infrastructure.Configurations;
using Mapster;

namespace GreenFood.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _manager;

        public CategoryService(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<Guid> CreateCategoryAsync(string categoryName)
        {
            if (_manager.Categories.CategoryByNameExisted(categoryName))
                throw new CategoryException("This category already exists!");

            Category category = new Category()
            {
                Name = categoryName
            };

            await _manager.Categories.AddAsync(category);

            await _manager.SaveChangesAsync();

            return category.Id;
        }

        public async Task DeleteCategoryByIdAsync(Guid categoryId)
        {
            var category = await _manager.Categories.GetByIdAsync(categoryId);

            if (category is null)
                throw new Exception();

            await _manager.Categories.RemoveAsync(category);

            await _manager.SaveChangesAsync();
        }

        public async Task<IEnumerable<OutputCategoryDto>> GetAllCategories()
        {
            var categories = await Task.Run( ()=> _manager.Categories.GetAll());

            return categories.Adapt<IEnumerable<OutputCategoryDto>>();

        }
    }
}
