using GreenFood.Domain.Contracts;
using GreenFood.Domain.Models;

namespace GreenFood.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(
            ApplicationContext context) : base(context)
        {
        }
    }
}
