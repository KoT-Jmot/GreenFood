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
using Microsoft.IdentityModel.Tokens;

namespace GreenFood.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ProductValidation _productValidator;

        public ProductService(
            IRepositoryManager repositoryManager,
            ProductValidation productValidator)
        {
            _repositoryManager = repositoryManager;
            _productValidator = productValidator;
        }

        public async Task<OutputProductDto> GetProductByIdAsync(
            Guid productId,
            CancellationToken cancellationToken)
        {
            var product = await _repositoryManager.Products.GetByIdAsync(productId, trackChanges: false, cancellationToken);

            if (product is null)
                throw new EntityNotFoundException("Product was not found!");

            var outputProduct = product.Adapt<OutputProductDto>();

            return outputProduct;
        }

        public async Task<PagedList<OutputProductDto>> GetAllProductsAsync(
            ProductQueryDto productQuery,
            CancellationToken cancellationToken)
        {
            var products = _repositoryManager.Products.GetAll();

            if (!productQuery.Header.IsNullOrEmpty())
                products = products.Where(p => p.Header!.Contains(productQuery.Header));

            products
                .NotNullWhere(p => p.Count, productQuery.Count)
                .NotNullWhere(p => p.Price, productQuery.Price)
                .NotNullWhere(p => p.CategoryId, productQuery.CategoryId);

            products = products.OrderBy(p => p.Header);
            var totalCount = await products.CountAsync(cancellationToken);

            var pagingProducts = await products
                                        .Skip((productQuery.PageNumber - 1) * productQuery.PageSize)
                                        .Take(productQuery.PageSize)
                                        .ToListAsync(cancellationToken);

            var outputProducts = pagingProducts.Adapt<IEnumerable<OutputProductDto>>();
            var productsWithMetaData = PagedList<OutputProductDto>.ToPagedList(outputProducts, productQuery.PageNumber, totalCount, productQuery.PageSize);

            return productsWithMetaData;
        }

        public async Task<Guid> CreateProductBySallerIdAsync(
            ProductDto productDto,
            string sellerId,
            CancellationToken cancellationToken)
        {
            await _productValidator.ValidateAndThrowAsync(productDto, cancellationToken);

            var category = await _repositoryManager.Categories.GetByIdAsync(productDto.CategoryId, trackChanges: false, cancellationToken);

            if (category is null)
                throw new EntityNotFoundException("Category was not found!");

            var product = productDto.Adapt<Product>();
            product.CreatedDate = DateTime.UtcNow;
            product.SellerId = sellerId;

            await _repositoryManager.Products.AddAsync(product, cancellationToken);
            await _repositoryManager.SaveChangesAsync(cancellationToken);

            return product.Id;
        }

        public async Task DeleteProductByIdAndSallerIdAsync(
            string sallerId,
            Guid productId,
            CancellationToken cancellationToken)
        {
            var product = await _repositoryManager.Products.GetProductByIdAndUserIdAsync(productId, sallerId, trackChanges: false, cancellationToken);

            if (product is null)
                throw new EntityNotFoundException("Product was not found!");

            await _repositoryManager.Products.RemoveAsync(product, cancellationToken);
            await _repositoryManager.SaveChangesAsync(cancellationToken);
        }

        public async Task<Guid> UpdateProductByIdAndSallerIdAsync(
            Guid productId,
            ProductDto productDto,
            string sallerId,
            CancellationToken cancellationToken)
        {
            await _productValidator.ValidateAndThrowAsync(productDto, cancellationToken);

            var category = await _repositoryManager.Categories.GetByIdAsync(productDto.CategoryId, trackChanges: false, cancellationToken);

            if (category is null)
                throw new EntityNotFoundException("Category was not found!");

            var updatingProduct = await _repositoryManager.Products.GetByIdAsync(productId, trackChanges: true, cancellationToken);

            if (updatingProduct is null)
                throw new EntityNotFoundException("product was not found!");

            if (!updatingProduct.SellerId!.Equals(sallerId))
                throw new RequestAccessException();

            updatingProduct = productDto.Adapt<Product>();

            return productId;
        }
    }
}
