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

        public bool CategoryByNameExisted(string name)
        {
            return GetByQueryable(c => c.Name == name).Any();
        }
        public bool CategoryByIdExisted(Guid categoruId)
        {
            return GetByQueryable(c => c.Id == categoruId).Any();
        }
    }
}
