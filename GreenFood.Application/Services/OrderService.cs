using FluentValidation;
using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.Extensions;
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
        private readonly IRepositoryManager _repositoryManager;
        private readonly AddOrderValidator _orderValidator;

        public OrderService(
            IRepositoryManager repositoryManager,
            AddOrderValidator orderValidator)
        {
            _repositoryManager = repositoryManager;
            _orderValidator = orderValidator;
        }

        public async Task<OutputOrderDto> GetOrderByIdAsync(
            Guid orderId,
            CancellationToken cancellationToken)
        {
            var order = await _repositoryManager.Orders.GetByIdAsync(orderId,trackChanges: false, cancellationToken);

            if (order is null)
                throw new EntityNotFoundException("Order was not found!");

            var outputOrder = order.Adapt<OutputOrderDto>();

            return outputOrder;
        }

        public async Task<PagedList<OutputOrderDto>> GetAllOrdersAsync(
            OrderQueryDto orderQuery,
            CancellationToken cancellationToken)
        {
            var orders = _repositoryManager.Orders.GetAll();

            orders.NotNullWhere(o => o.ProductId, orderQuery.ProductId);

            orders = orders.OrderBy(o=>o.CreateDate);
            var totalCount = await orders.CountAsync(cancellationToken);

            var pagingOrders = await orders
                                        .Skip((orderQuery.PageNumber - 1) * orderQuery.PageSize)
                                        .Take(orderQuery.PageSize)
                                        .ToListAsync(cancellationToken);

            var outputOrders = pagingOrders.Adapt<IEnumerable<OutputOrderDto>>();
            var ordersWithMetaData = PagedList<OutputOrderDto>.ToPagedList(outputOrders, orderQuery.PageNumber, totalCount, orderQuery.PageSize);

            return ordersWithMetaData;
        }

        public async Task<Guid> CreateOrderByUserIdAsync(
            OrderDto orderDto,
            string customerId,
            CancellationToken cancellationToken)
        {
            await _orderValidator.ValidateAndThrowAsync(orderDto, cancellationToken);

            var product = await _repositoryManager.Products.GetByIdAsync(orderDto.ProductId, trackChanges: false, cancellationToken);

            if (product is null)
                throw new EntityNotFoundException("Product was not found!");

            if (product.Count < orderDto.Count)
                throw new ProductCountException();

            if (product.SellerId!.Equals(customerId))
                throw new OrderCustomerException();

            var order = orderDto.Adapt<Order>();
            order.CreateDate = DateTime.Now;
            order.CustomerId = customerId;

            await _repositoryManager.Orders.AddAsync(order, cancellationToken);
            await _repositoryManager.SaveChangesAsync(cancellationToken);

            product.Count -= order.Count;

            return order.Id;
        }

        public async Task DeleteOrderByIdAndUserIdAsync(
            string userId,
            Guid orderId,
            CancellationToken cancellationToken)
        {
            var order = await _repositoryManager.Orders.GetOrderByIdAndUserIdAsync(orderId, userId, trackChanges: false, cancellationToken);

            if (order is null)
                throw new EntityNotFoundException("Order was not found!");

            await _repositoryManager.Orders.RemoveAsync(order, cancellationToken);
            await _repositoryManager.SaveChangesAsync(cancellationToken);
        }
    }
}
