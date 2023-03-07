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
using Microsoft.IdentityModel.Tokens;

namespace GreenFood.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryManager _manager;
        private readonly ProductValidation _ProductValidator;

        public ProductService(
            IRepositoryManager manager,
            ProductValidation ProductValidator)
        {
            _manager = manager;
            _ProductValidator = ProductValidator;
        }

        public async Task<OutputProductDto> GetProductByIdAsync(
            Guid productId,
            CancellationToken cancellationToken)
        {
            var product = await _manager.Products.GetByIdAsync(productId, false, cancellationToken);

            if (product is null)
                throw new EntityNotFoundException("Product was not found!");

            var outputProduct = product.Adapt<OutputProductDto>();

            return outputProduct;
        }

        public async Task<PagedList<OutputProductDto>> GetAllProductsAsync(
            ProductQueryDto productQuery,
            CancellationToken cancellationToken)
        {
            var products = _manager.Products.GetAll();

            if (!productQuery.Header.IsNullOrEmpty())
                products = products.Where(p => p.Header!.Contains(productQuery.Header));

            if (productQuery.Count is not null)
                products = products.Where(p => p.Count == productQuery.Count);

            if (productQuery.Price is not null)
                products = products.Where(p => p.Price == productQuery.Price);

            if (productQuery.CategoryId is not null)
                products = products.Where(p => p.CategoryId == productQuery.CategoryId);

            products = products.OrderBy(p => p.Header);
            var totalCount = products.CountAsync();

            var pagingProducts = await products
                                        .Skip((productQuery.pageNumber - 1) * productQuery.pageSize)
                                        .Take(productQuery.pageSize)
                                        .ToListAsync(cancellationToken);

            var outputProducts = pagingProducts.Adapt<IEnumerable<OutputProductDto>>();

            var count = await totalCount;

            var productsWithMetaData = PagedList<OutputProductDto>.ToPagedList(outputProducts, productQuery.pageNumber, count, productQuery.pageSize);

            return productsWithMetaData;
        }

        public async Task<Guid> CreateProductByUserIdAsync(
            ProductDto productDto,
            string sellerId,
            CancellationToken cancellationToken)
        {
            await _ProductValidator.ValidateAndThrowAsync(productDto, cancellationToken);

            var category = await _manager.Categories.GetByIdAsync(productDto.CategoryId, false, cancellationToken);

            if (category is null)
                throw new EntityNotFoundException("Category was not found!");

            var product = productDto.Adapt<Product>();
            product.SellerId = sellerId;

            await _manager.Products.AddAsync(product, cancellationToken);
            await _manager.SaveChangesAsync(cancellationToken);

            return product.Id;
        }

        public async Task DeleteProductByIdAndUserIdAsync(
            string userId,
            Guid productId,
            CancellationToken cancellationToken)
        {
            var product = await _manager.Products.GetProductByIdAndUserIdAsync(productId, userId, false, cancellationToken);

            if (product is null)
                throw new EntityNotFoundException("Product was not found!");

            await _manager.Products.RemoveAsync(product, cancellationToken);
            await _manager.SaveChangesAsync(cancellationToken);
        }
    }
}
