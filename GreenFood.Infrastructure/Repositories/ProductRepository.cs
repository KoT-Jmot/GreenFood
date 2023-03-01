using GreenFood.Domain.Contracts;
using GreenFood.Domain.Models;

namespace GreenFood.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(
            ApplicationContext context) : base(context)
        {
        }

        public IQueryable<Product> GetProductsByUserId(
            string userId,
            bool trackChanges = false)
        {
            return GetByQueryable(p => p.SellerId!.Equals(userId), trackChanges);
        }
        
        public Product? GetProductByIdAndUserId(
            Guid productId,
            string userId,
            bool trackChanges = false)
        {
            return GetByQueryable(p => p.Id == productId && p.SellerId == userId).FirstOrDefault();
        }
    }
}
