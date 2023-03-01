using GreenFood.Application.DTO.InputDto;

namespace GreenFood.Application.Contracts
{
    public interface IProductService
    {
        Task CreateProductByUserId(ProductForAddDto productDto, string userId);
        Task DeleteProductByIdAndUserIdAsync(string userId, Guid productId);
    }
}
