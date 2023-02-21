using GreenFood.Domain.Contracts;
using GreenFood.Domain.Models;

namespace GreenFood.Infrastructure.Repositories
{
    public class TypeOfProductRepository : BaseRepository<TypeOfProduct>, ITypeOfProductRepository
    {
        public TypeOfProductRepository(
            ApplicationContext context) : base(context)
        {
        }
    }
}
