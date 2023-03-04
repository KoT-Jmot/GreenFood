using GreenFood.Domain.Models;

namespace GreenFood.Domain.Contracts
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<Order?> GetOrderByIdAndUserIdAsync(
            Guid orderId,
            string userId,
            CancellationToken cancellationToken = default,
            bool trackChanges = false);
    }
}
