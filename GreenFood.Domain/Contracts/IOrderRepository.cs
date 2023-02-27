using GreenFood.Domain.Models;

namespace GreenFood.Domain.Contracts
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public IQueryable<Order> GetOrdersByUserId(
            string userId,
            bool trackChenges = false);
    }
}
