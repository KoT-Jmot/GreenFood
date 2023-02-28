using GreenFood.Domain.Contracts;

namespace GreenFood.Infrastructure.Configurations
{
    public interface IRepositoryManager
    {
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        ICategoryRepository Categories { get; }
        Task SaveChangesAsync();
    }
}
