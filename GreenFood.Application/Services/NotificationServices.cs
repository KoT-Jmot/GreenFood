using GreenFood.Application.Contracts;
using GreenFood.Infrastructure.Configurations;

namespace GreenFood.Application.Services
{
    public class NotificationServices : INotificationServices
    {
        private readonly IRepositoryManager _repositoryManager;

        public NotificationServices(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task DeleteLatestOrdersAsync(CancellationToken cancellationToken = default)
        {
            var deletingOrders = _repositoryManager.Orders
                                                   .GetAll()
                                                   .Where(o => o.CreateDate.AddDays(30).Date <= DateTime.UtcNow.Date);

            await _repositoryManager.Orders.RemoveRangeAsync(deletingOrders);
            await _repositoryManager.SaveChangesAsync();
        }
    }
}
