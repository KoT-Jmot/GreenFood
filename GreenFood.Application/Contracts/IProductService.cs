using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.RequestFeatures;

namespace GreenFood.Application.Contracts
{
    public interface IProductService
    {
        Task<PagedList<OutputProductDto>> GetAllProductsAsync(
            ProductQueryDto productQuery,
            CancellationToken cancellationToken);
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
