using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.DTO.ServicesDto;
using GreenFood.Domain.Models;
using Mapster;
using System.Collections.Generic;

namespace GreenFood.Application.Mapster
{
    public class OrdersMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<InputOrderDto, OrderDto>();
            config.NewConfig<OrderDto, Order>();
            config.NewConfig<IEnumerable<Order>, IEnumerable<OutputOrderDto>>();
        }
    }
}
