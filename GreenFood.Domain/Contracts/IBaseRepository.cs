namespace GreenFood.Domain.Contracts
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> GetAll(bool trackChanges = false);
        Task<T?> GetByIdAsync(Guid id, bool trackChanges = false);
        Task AddAsync(T obj);
        Task RemoveAsync(T obj);
        Task RemoveRangeAsync(IEnumerable<T> obj);
    }
}
