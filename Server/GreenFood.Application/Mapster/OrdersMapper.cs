using GreenFood.Application.DTO.InputDto.OrderDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Domain.Models;
using Mapster;

namespace GreenFood.Application.Mapster
{
    public class OrdersMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<OrderDto, Order>();
            config.NewConfig<Order, OutputOrderDto>();
        }
    }
}
