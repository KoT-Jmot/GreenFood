﻿using GreenFood.Domain.Models;

namespace GreenFood.Domain.Contracts
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        IQueryable<Product> GetProductsByUserId(
            string userId,
            bool trackChanges = false);
    }
}