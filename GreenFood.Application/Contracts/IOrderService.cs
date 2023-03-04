using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;

namespace GreenFood.Application.Contracts
{
    public interface IOrderService
    {
        Task<OutputOrderDto> GetOrderByIdAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);
        Task<IEnumerable<OutputOrderDto>> GetAllOrdersAsync(CancellationToken cancellationToken = default);
        Task<Guid> CreateOrderByUserIdAsync(
            OrderDto orderDto,
            string customerId,
            CancellationToken cancellationToken = default);
        Task DeleteOrderByIdAndUserIdAsync(
            string userId,
            Guid orderId,
            CancellationToken cancellationToken = default);
    }
}
