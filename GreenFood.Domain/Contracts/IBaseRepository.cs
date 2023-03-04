namespace GreenFood.Domain.Contracts
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> GetAll(bool trackChanges = false);
        Task<T?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default,
            bool trackChanges = false);
        Task AddAsync(
            T entity,
            CancellationToken cancellationToken = default);
        Task RemoveAsync(
            T entity,
            CancellationToken cancellationToken = default);
        Task RemoveRangeAsync(
            IEnumerable<T> entity,
            CancellationToken cancellationToken = default);
    }
}
