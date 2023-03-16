using GreenFood.Application.DTO.InputDto.CategoryDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Domain.Models;
using Mapster;

namespace GreenFood.Application.Mapster
{
    internal class CategoriesMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CategoryDto, Category>();
            config.NewConfig<Category, OutputCategoryDto>();
        }
    }
}
