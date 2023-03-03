using GreenFood.Domain.Contracts;
using GreenFood.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenFood.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(
            ApplicationContext context) : base(context)
        {
        }

        public async Task<Product?> GetProductByIdAndUserIdAsync(
            Guid productId,
            string userId,
            bool trackChanges = false)
        {
            return await GetByQueryable(p => p.Id.Equals(productId) && p.SellerId.Equals(userId), trackChanges).FirstOrDefaultAsync();
        }
    }
}
