using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;

namespace GreenFood.Application.Contracts
{
    public interface IProductService
    {
        Task<OutputProductDto> GetProductByIdAsync(Guid productId);
        Task<IEnumerable<OutputProductDto>> GetAllProductsAsync();
        Task<Guid> CreateProductByUserIdAsync(
            ProductDto productDto,
            string sellerId);
        Task DeleteProductByIdAndUserIdAsync(
            string userId,
            Guid productId);
    }
}
