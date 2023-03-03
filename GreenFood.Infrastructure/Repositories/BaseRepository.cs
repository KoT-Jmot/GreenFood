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

        public async Task AddAsync(T entity)
        {
            await Set.AddAsync(entity);
        }

        public async Task RemoveAsync(T entity)
        {
            await Task.Run(() => Set.Remove(entity));
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entity)
        {
            await Task.Run(() => Set.RemoveRange(entity));
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
