using GreenFood.Domain.Models;

namespace GreenFood.Domain.Contracts
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        IQueryable<Order> GetOrdersByUserId(
            string userId,
            bool trackChanges = false);

        Order? GetOrderByIdAndUserId(
            Guid orderId,
            string userId,
            bool trackChanges = false);
    }
}
