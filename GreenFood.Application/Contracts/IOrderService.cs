using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;

namespace GreenFood.Application.Contracts
{
    public interface IOrderService
    {
        Task<IEnumerable<OutputOrderDto>> GetAllOrdersAsync(CancellationToken cancellationToken);
        Task<OutputOrderDto> GetOrderByIdAsync(
            Guid orderId,
            CancellationToken cancellationToken);
        Task<Guid> CreateOrderByUserIdAsync(
            OrderDto orderDto,
            string customerId,
            CancellationToken cancellationToken);
        Task DeleteOrderByIdAndUserIdAsync(
            string userId,
            Guid orderId,
            CancellationToken cancellationToken);
    }
}
