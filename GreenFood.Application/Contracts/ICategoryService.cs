using GreenFood.Application.DTO;

namespace GreenFood.Application.Contracts
{
    public interface ICategoryService
    {
        public Task CreateCategoryAsync(CategoryForAddDto productTypeDto);
    }
}
