using GreenFood.Application.DTO;
using GreenFood.Domain.Models;
using Mapster;

namespace GreenFood.Application.Mapster
{
    public class ProductMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ProductForAddDto, Product>();
            config.NewConfig<ProductForDeleteDto, Product>();
        }
    }
}
