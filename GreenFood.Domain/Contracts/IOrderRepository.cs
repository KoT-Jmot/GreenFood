using GreenFood.Domain.Models;

namespace GreenFood.Domain.Contracts
{
    public interface IOrderRepository
    {
        public IQueryable<Order> GetOrdersByUserId(
            string userId,
            bool trackChenges = false);
    }
}
