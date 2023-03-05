using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;

namespace GreenFood.Application.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<OutputProductDto>> GetAllProductsAsync(CancellationToken cancellationToken);
        Task<OutputProductDto> GetProductByIdAsync(
            Guid productId,
            CancellationToken cancellationToken);
        Task<Guid> CreateProductByUserIdAsync(
            ProductDto productDto,
            string sellerId,
            CancellationToken cancellationToken);
        Task DeleteProductByIdAndUserIdAsync(
            string userId,
            Guid productId,
            CancellationToken cancellationToken);
    }
}
