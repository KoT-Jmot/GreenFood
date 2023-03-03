using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;

namespace GreenFood.Application.Contracts
{
    public interface IOrderService
    {
        Task<OutputOrderDto> GetOrderByIdAsync(Guid orderId);
        Task<IEnumerable<OutputOrderDto>> GetAllOrdersAsync();
        Task<Guid> CreateOrderByUserIdAsync(
            OrderDto orderDto,
            string customerId);
        Task DeleteOrderByIdAndUserIdAsync(
            string userId,
            Guid orderId);
    }
}
