using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.DTO.ServicesDto;

namespace GreenFood.Application.Contracts
{
    public interface IOrderService
    {
        Task<Guid> CreateOrder(OrderDto orderDto);
        Task DeleteOrderByIdAndUserId(
            string userId,
            Guid orderId);
        Task<IEnumerable<OutputOrderDto>> GetOrdersByUserId(
            string userId,
            bool trackChanges = false);
    }
}
