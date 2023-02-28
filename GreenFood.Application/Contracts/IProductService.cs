using GreenFood.Application.DTO;

namespace GreenFood.Application.Contracts
{
    public interface IProductService
    {
        Task CreateProductByUserId(ProductForAddDto productDto, string userId);
        Task DeleteProductAsync(ProductForDeleteDto productDto, string userId);
    }
}
