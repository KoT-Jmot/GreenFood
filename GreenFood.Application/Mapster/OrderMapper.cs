using GreenFood.Application.DTO.InputDto;
using GreenFood.Domain.Models;
using Mapster;

namespace GreenFood.Application.Mapster
{
    public class OrderMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<OrderForAddDto, Order>();
        }
    }
}
