using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.DTO.ServicesDto;
using GreenFood.Domain.Models;
using GreenFood.Infrastructure.Configurations;
using Mapster;

namespace GreenFood.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryManager _manager;

        public OrderService(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<Guid> CreateOrder(OrderDto orderDto)
        {
            var product = await _manager.Products.GetByIdAsync(orderDto.ProductId, true);

            if (product is null || product.Count<orderDto.Count)
                throw new Exception();

            var order = orderDto.Adapt<Order>();
            order.CreateDate = DateTime.Now;

            await _manager.Orders.AddAsync(order);

            product.Count -= order.Count;

            await _manager.SaveChangesAsync();

            return order.Id;
        }

        public async Task DeleteOrderByIdAndUserId(
            string userId,
            Guid orderId)
        {
            var order = _manager.Orders.GetOrderByIdAndUserId(orderId, userId);

            if (order is null)
                throw new Exception();

            await _manager.Orders.RemoveAsync(order);

            await _manager.SaveChangesAsync();
        }

        public async Task<IEnumerable<OutputOrderDto>> GetOrdersByUserId(
            string userId,
            bool trackChanges = false)
        {
            var orders = await Task.Run(() => _manager.Orders.GetOrdersByUserId(userId, trackChanges));

            return orders.Adapt<IEnumerable<OutputOrderDto>>();
        }
    }
}
