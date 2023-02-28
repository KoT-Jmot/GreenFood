﻿using GreenFood.Application.Contracts;
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

            await _manager.Categories.AddAsync(new Category() { Name = categoryName });

            await _manager.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            await _manager.Categories.RemoveAsync(new Category() { Id = categoryId });

            await _manager.SaveChangesAsync();
        }
    }
}