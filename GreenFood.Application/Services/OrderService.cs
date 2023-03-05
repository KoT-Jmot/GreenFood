using FluentValidation;
using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.Validation;
using GreenFood.Domain.Exceptions;
using GreenFood.Domain.Models;
using GreenFood.Infrastructure.Configurations;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GreenFood.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryManager _manager;
        private readonly AddOrderValidator _OrderValidator;

        public OrderService(
            IRepositoryManager manager,
            AddOrderValidator OrderValidator)
        {
            _manager = manager;
            _OrderValidator = OrderValidator;
        }

        public async Task<OutputOrderDto> GetOrderByIdAsync(
            Guid orderId,
            CancellationToken cancellationToken)
        {
            var order = await _manager.Orders.GetByIdAsync(orderId, cancellationToken);

            if (order is null)
                throw new EntityNotFoundException("Order was not found!");

            var outputOrder = order.Adapt<OutputOrderDto>();

            return outputOrder;
        }

        public async Task<IEnumerable<OutputOrderDto>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            var orders = await _manager.Orders.GetAll().ToListAsync(cancellationToken);

            var outputOrders = orders.Adapt<IEnumerable<OutputOrderDto>>();

            return outputOrders;
        }

        public async Task<Guid> CreateOrderByUserIdAsync(
            OrderDto orderDto,
            string customerId,
            CancellationToken cancellationToken)
        {
            await _OrderValidator.ValidateAndThrowAsync(orderDto, cancellationToken);

            var product = await _manager.Products.GetByIdAsync(orderDto.ProductId, cancellationToken, true);

            if (product is null)
                throw new EntityNotFoundException("Product was not found!");

            if (product.Count < orderDto.Count)
                throw new ProductCountException();

            var order = orderDto.Adapt<Order>();
            order.CreateDate = DateTime.Now;
            order.CustomerId = customerId;

            await _manager.Orders.AddAsync(order, cancellationToken);
            await _manager.SaveChangesAsync(cancellationToken);

            product.Count -= order.Count;

            return order.Id;
        }

        public async Task DeleteOrderByIdAndUserIdAsync(
            string userId,
            Guid orderId,
            CancellationToken cancellationToken)
        {
            var order = await _manager.Orders.GetOrderByIdAndUserIdAsync(orderId, userId, cancellationToken);

            if (order is null)
                throw new EntityNotFoundException("Order was not found!");

            await _manager.Orders.RemoveAsync(order, cancellationToken);
            await _manager.SaveChangesAsync(cancellationToken);
        }
    }
}
