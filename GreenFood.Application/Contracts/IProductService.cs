using GreenFood.Application.DTO;

namespace GreenFood.Application.Contracts
{
    public interface IProductService
    {
        public Task CreateProduct(ProductForAddDto productDto);
    }
}
