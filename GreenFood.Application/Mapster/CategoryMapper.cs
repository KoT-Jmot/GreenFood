using GreenFood.Application.DTO;
using GreenFood.Domain.Models;
using Mapster;

namespace GreenFood.Application.Mapster
{
    public class CategoryMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CategoryForAddDto, Category>();
        }
    }
}
