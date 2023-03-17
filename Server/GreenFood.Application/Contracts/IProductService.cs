using GreenFood.Application.DTO.InputDto.ProductDto;
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
        Task<Guid> CreateProductBySallerIdAsync(
            ProductDto productDto,
            string sellerId,
            CancellationToken cancellationToken);
        Task DeleteProductByIdAndSallerIdAsync(
            string userId,
            Guid productId,
            CancellationToken cancellationToken);
        Task<Guid> UpdateProductByIdAndSallerIdAsync(
            Guid productId,
            ProductDto productDto,
            string sallerId,
            CancellationToken cancellationToken);
    }
}
