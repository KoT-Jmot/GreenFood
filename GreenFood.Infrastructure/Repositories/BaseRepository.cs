using GreenFood.Domain.Contracts;
using GreenFood.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GreenFood.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _set;

        protected BaseRepository(ApplicationContext context)
        {
            _set = context.Set<T>();
        }

        public  IQueryable<T> GetAll(bool trackChanges = false)
        {
            return GetByQueryable(_=>true, trackChanges);
        }
 
        public async Task<T?> GetByIdAsync(Guid id, bool trackChanges = false)
        {
            return trackChanges
                        ? await _set.FirstOrDefaultAsync(t => t.Id.Equals(id))
                        : await _set.AsNoTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));
        }

        public async Task AddAsync(T obj)
        {
            await _set.AddAsync(obj);
        }

        public async Task AddRangeAsync(IEnumerable<T> obj)
        {
            await _set.AddRangeAsync(obj);
        }

        public async Task RemoveAsync(T obj)
        {
            await Task.Run(() => _set.Remove(obj));
        }

        public async Task RemoveRangeAsync(IEnumerable<T> obj)
        {
            await Task.Run(() => _set.RemoveRange(obj));
        }

        protected virtual IQueryable<T> GetByQueryable(
            Expression<Func<T,bool>> expression, bool trackChanges = false)
        {
            var items = _set.Where(expression);

            return trackChanges
                        ? items
                        : items.AsNoTracking();
        }
    }
}
