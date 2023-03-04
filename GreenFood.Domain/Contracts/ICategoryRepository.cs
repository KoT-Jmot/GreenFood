using GreenFood.Domain.Models;

namespace GreenFood.Domain.Contracts
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category?> GetCategoryByNameAsync(
            string name,
            CancellationToken cancellationToken = default,
            bool TrackChanges = false);
    }
}
