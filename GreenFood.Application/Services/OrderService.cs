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
        private readonly AddOrderValidator _addRule;

        public OrderService(
            IRepositoryManager manager,
            AddOrderValidator addRule)
        {
            _manager = manager;
            _addRule = addRule;
        }

        public async Task<OutputOrderDto> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _manager.Orders.GetByIdAsync(orderId);

            if (order is null)
                throw new EntityNotFoundException("Order was not found!");

            return order.Adapt<OutputOrderDto>();
        }

        public async Task<IEnumerable<OutputOrderDto>> GetAllOrdersAsync()
        {
            var orders = await _manager.Orders.GetAll().ToListAsync();

            return orders.Adapt<IEnumerable<OutputOrderDto>>();
        }

        public async Task<Guid> CreateOrderByUserIdAsync(
            OrderDto orderDto,
            string customerId)
        {
            await _addRule.ValidateAndThrowAsync(orderDto);

            var product = await _manager.Products.GetByIdAsync(orderDto.ProductId, true);

            if (product is null)
                throw new EntityNotFoundException("Product was not found!");

            if (product.Count < orderDto.Count)
                throw new ProductCountException();

            var order = orderDto.Adapt<Order>();
            order.CreateDate = DateTime.Now;
            order.CustomerId = customerId;

            await _manager.Orders.AddAsync(order);

            product.Count -= order.Count;

            await _manager.SaveChangesAsync();

            return order.Id;
        }

        public async Task DeleteOrderByIdAndUserIdAsync(
            string userId,
            Guid orderId)
        {
            var order = await _manager.Orders.GetOrderByIdAndUserIdAsync(orderId, userId);

            if (order is null)
                throw new EntityNotFoundException("Order was not found!");

            await _manager.Orders.RemoveAsync(order);

            await _manager.SaveChangesAsync();
        }
    }
}
