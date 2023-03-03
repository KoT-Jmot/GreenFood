using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.DTO.ServicesDto;
using GreenFood.Domain.Models;
using Mapster;

namespace GreenFood.Application.Mapster
{
    public class ProductsMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<InputProductDto, ProductDto>();
            config.NewConfig<ProductDto, Product>();
            config.NewConfig<IEnumerable<Product>, IEnumerable<OutputProductDto>>();
        }
    }
}
