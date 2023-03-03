using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.DTO.ServicesDto;
using GreenFood.Domain.Exceptions;
using GreenFood.Domain.Models;
using GreenFood.Infrastructure.Configurations;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace GreenFood.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryManager _manager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductService(
            IRepositoryManager manager,
            UserManager<ApplicationUser> userManager)
        {
            _manager = manager;
            _userManager = userManager;
        }

        public async Task<Guid> CreateProductByUserId(ProductDto productDto)
        {
            if (!_manager.Categories.CategoryByIdExisted(productDto.CategoryId))
                throw new CategoryException();

            var product = productDto.Adapt<Product>();

            await _manager.Products.AddAsync(product);

            await _manager.SaveChangesAsync();

            return product.Id;
        }

        public async Task DeleteProductByIdAndUserIdAsync(
            string userId,
            Guid productId)
        {
            var product = _manager.Products.GetProductByIdAndUserId(productId, userId);

            if (product is null)
                throw new Exception();

            await _manager.Products.RemoveAsync(product);

            await _manager.SaveChangesAsync();
        }

        public async Task<IEnumerable<OutputProductDto>> GetProductByUserEmail(
            string userEmail,
            bool trackChanges = false)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user is null)
                throw new Exception();

            var products = await Task.Run(() => _manager.Products.GetProductsByUserId(user.Id, trackChanges));

            return products.Adapt<IEnumerable<OutputProductDto>>();
        }
    }
}
