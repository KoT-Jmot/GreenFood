using GreenFood.Application.Contracts;
using GreenFood.Domain.Exceptions;
using GreenFood.Domain.Models;
using GreenFood.Infrastructure.Configurations;

namespace GreenFood.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _manager;

        public CategoryService(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task CreateCategoryAsync(string categoryName)
        {
            if (_manager.Categories.CategoryByNameExisted(categoryName))
                throw new CategoryException("This category already exists!");

            Category category = new Category()
            {
                Name = categoryName
            };

            await _manager.Categories.AddAsync(category);

            await _manager.SaveChangesAsync();
        }

        public async Task DeleteCategoryByIdAsync(Guid categoryId)
        {
            var category = await _manager.Categories.GetByIdAsync(categoryId);

            if (category is null)
                throw new Exception();

            await _manager.Categories.RemoveAsync(category);

            await _manager.SaveChangesAsync();
        }
    }
}
