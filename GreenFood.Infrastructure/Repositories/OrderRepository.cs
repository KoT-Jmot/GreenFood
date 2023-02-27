using GreenFood.Domain.Contracts;
using GreenFood.Domain.Models;

namespace GreenFood.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(
            ApplicationContext context) : base(context)
        {
        }

        public IQueryable<Order> GetOrdersByUserId(
            string userId,
            bool trackChanges = false)
        {
            return GetByQueryable(o=>o.CustomerId!.Equals(userId), trackChanges);
        }
    }
}
