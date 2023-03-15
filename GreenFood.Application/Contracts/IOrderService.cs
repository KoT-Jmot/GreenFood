using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.RequestFeatures;

namespace GreenFood.Application.Contracts
{
    public interface IOrderService
    {
        Task<PagedList<OutputOrderDto>> GetAllOrdersAsync(
            OrderQueryDto orderQuery,
            CancellationToken cancellationToken);
        Task<OutputOrderDto> GetOrderByIdAsync(
            Guid orderId,
            CancellationToken cancellationToken);
        Task<Guid> CreateOrderByCustomerIdAsync(
            OrderDto orderDto,
            string customerId,
            CancellationToken cancellationToken);
        Task DeleteOrderByIdAndCustomerIdAsync(
            string customerId,
            Guid orderId,
            CancellationToken cancellationToken);
    }
}
