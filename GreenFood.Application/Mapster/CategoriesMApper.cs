using GreenFood.Application.DTO.OutputDto;
using GreenFood.Domain.Models;
using Mapster;

namespace GreenFood.Application.Mapster
{
    internal class CategoriesMApper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<IEnumerable<Category>, IEnumerable<OutputCategoryDto>>();
        }
    }
}
