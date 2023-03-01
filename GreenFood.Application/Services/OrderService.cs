using FluentValidation;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.Validation;
using GreenFood.Domain.Models;
using GreenFood.Infrastructure.Configurations;
using Mapster;

namespace GreenFood.Application.Services
{
    public class OrderService
    {
        private readonly IRepositoryManager _manager;
        private readonly AddOrderValidator _addValidator;
        public OrderService(IRepositoryManager manager, AddOrderValidator addValidator)
        {
            _manager = manager;
            _addValidator = addValidator;
        }

        public async Task CreateOrder(OrderForAddDto orderDto)
        {
            await _addValidator.ValidateAndThrowAsync(orderDto);

            var product = await _manager.Products.GetByIdAsync(orderDto.ProductId, true);

            if (product is null || product.Count<orderDto.Count)
                throw new Exception();

            var order = orderDto.Adapt<Order>();
            order.CreateDate = DateTime.Now;

            await _manager.Orders.AddAsync(order);

            product.Count -= orderDto.Count;

            await _manager.SaveChangesAsync();
        }

        public async Task DeleteOrderByIdAndUserId(string userId, Guid orderId)
        {
            var order = _manager.Orders.GetOrderByIdAndUserId(orderId, userId);

            if (order is null)
                throw new Exception();

            await _manager.Orders.RemoveAsync(order);

            await _manager.SaveChangesAsync();
        }
    }
}
