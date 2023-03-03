using GreenFood.Domain.Models;

namespace GreenFood.Domain.Contracts
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product?> GetProductByIdAndUserIdAsync(
            Guid productId,
            string userId,
            bool trackChanges = false);
    }
}
