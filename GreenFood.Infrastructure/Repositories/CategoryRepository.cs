using GreenFood.Domain.Contracts;
using GreenFood.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenFood.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(
            ApplicationContext context) : base(context)
        { 
        }

        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            return await GetByQueryable(c => c.Name.Equals(name)).FirstOrDefaultAsync();
        }
    }
}
