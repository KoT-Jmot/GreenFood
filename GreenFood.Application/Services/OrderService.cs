using FluentValidation;
using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.RequestFeatures;
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
            var order = await _manager.Orders.GetByIdAsync(orderId, false, cancellationToken);

            if (order is null)
                throw new EntityNotFoundException("Order was not found!");

            var outputOrder = order.Adapt<OutputOrderDto>();

            return outputOrder;
        }

        public async Task<PagedList<OutputOrderDto>> GetAllOrdersAsync(
            OrderQueryDto orderQuery,
            CancellationToken cancellationToken)
        {
            var orders = _manager.Orders.GetAll();

            if(orderQuery.ProductId is not null)
                orders = orders.Where(o=>o.ProductId==orderQuery.ProductId);

            orders = orders.OrderBy(o=>o.CreateDate);
            var totalCount = orders.CountAsync();

            var pagingOrders = await orders
                                        .Skip((orderQuery.pageNumber - 1) * orderQuery.pageSize)
                                        .Take(orderQuery.pageSize)
                                        .ToListAsync(cancellationToken);

            var outputOrders = pagingOrders.Adapt<IEnumerable<OutputOrderDto>>();

            var count = await totalCount;

            var ordersWithMetaData = PagedList<OutputOrderDto>.ToPagedList(outputOrders, orderQuery.pageNumber, count, orderQuery.pageSize);

            return ordersWithMetaData;
        }

        public async Task<Guid> CreateOrderByUserIdAsync(
            OrderDto orderDto,
            string customerId,
            CancellationToken cancellationToken)
        {
            await _OrderValidator.ValidateAndThrowAsync(orderDto, cancellationToken);

            var product = await _manager.Products.GetByIdAsync(orderDto.ProductId, true, cancellationToken);

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
            var order = await _manager.Orders.GetOrderByIdAndUserIdAsync(orderId, userId, false, cancellationToken);

            if (order is null)
                throw new EntityNotFoundException("Order was not found!");

            await _manager.Orders.RemoveAsync(order, cancellationToken);
            await _manager.SaveChangesAsync(cancellationToken);
        }
    }
}
