using GreenFood.Domain.Contracts;
using GreenFood.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GreenFood.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> Set;

        protected BaseRepository(ApplicationContext context)
        {
            Set = context.Set<T>();
        }

        public  IQueryable<T> GetAll(bool trackChanges = false)
        {
            return GetByQueryable(_=>true, trackChanges);
        }
 
        public async Task<T?> GetByIdAsync(Guid id, bool trackChanges = false)
        {
            return trackChanges
                        ? await Set.FirstOrDefaultAsync(t => t.Id.Equals(id))
                        : await Set.AsNoTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));
        }

        public async Task AddAsync(T obj)
        {
            await Set.AddAsync(obj);
        }

        public async Task RemoveAsync(T obj)
        {
            await Task.Run(() => Set.Remove(obj));
        }

        public async Task RemoveRangeAsync(IEnumerable<T> obj)
        {
            await Task.Run(() => Set.RemoveRange(obj));
        }

        protected virtual IQueryable<T> GetByQueryable(
            Expression<Func<T,bool>> expression, bool trackChanges = false)
        {
            var items = Set.Where(expression);

            return trackChanges
                        ? items
                        : items.AsNoTracking();
        }
    }
}
