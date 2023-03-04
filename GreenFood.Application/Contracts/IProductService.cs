using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;

namespace GreenFood.Application.Contracts
{
    public interface IProductService
    {
        Task<OutputProductDto> GetProductByIdAsync(
            Guid productId,
            CancellationToken cancellationToken = default);
        Task<IEnumerable<OutputProductDto>> GetAllProductsAsync(CancellationToken cancellationToken = default);
        Task<Guid> CreateProductByUserIdAsync(
            ProductDto productDto,
            string sellerId,
            CancellationToken cancellationToken = default);
        Task DeleteProductByIdAndUserIdAsync(
            string userId,
            Guid productId,
            CancellationToken cancellationToken = default);
    }
}
