using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.DTO.ServicesDto;
using GreenFood.Domain.Models;

namespace GreenFood.Application.Contracts
{
    public interface IProductService
    {
        Task<Guid> CreateProductByUserId(ProductDto productDto);
        Task DeleteProductByIdAndUserIdAsync(string userId, Guid productId);
        Task<IEnumerable<OutputProductDto>> GetProductByUserEmail(string userEmail, bool trackChanges = false);
    }
}
