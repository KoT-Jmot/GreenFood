﻿using GreenFood.Domain.Contracts;
using GreenFood.Infrastructure.Configurations;

namespace GreenFood.Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationContext _context;

        private IProductRepository? _productRepository;
        private IOrderRepository? _orderRepository;
        private ICategoryRepository? _categoryRepository;

        public RepositoryManager(ApplicationContext context)
        {
            _context = context;
        }

        public IProductRepository Products =>
            _productRepository??=new ProductRepository(_context);

        public IOrderRepository Orders =>
            _orderRepository ??= new OrderRepository(_context);

        public ICategoryRepository Categories =>
                _categoryRepository ??= new CategoryRepository(_context);

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}