using GreenFood.Domain.Models;

namespace GreenFood.Domain.Contracts
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        bool CategoryByNameExisted(string name);
        bool CategoryByIdExisted(Guid categoruId);
    }
}
