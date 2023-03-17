using GreenFood.Domain.Contracts;
using GreenFood.Domain.Models;
using GreenFood.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GreenFood.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(
            ApplicationContext context) : base(context)
        {
        }

        public async Task<Order?> GetOrderByIdAndUserIdAsync(
            Guid orderId,
            string userId,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return await GetByQueryable(o => o.Id.Equals(orderId) && o.CustomerId!.Equals(userId), trackChanges).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
