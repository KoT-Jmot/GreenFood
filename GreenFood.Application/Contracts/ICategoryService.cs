using GreenFood.Application.DTO;

namespace GreenFood.Application.Contracts
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(string categoryName);
        Task DeleteCategoryByIdAsync(Guid categoryId);
    }
}
