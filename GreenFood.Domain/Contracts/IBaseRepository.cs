namespace GreenFood.Domain.Contracts
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> GetAll(bool trackChanges = false);
        Task<T?> GetByIdAsync(Guid id, bool trackChanges = false);
    }
}
