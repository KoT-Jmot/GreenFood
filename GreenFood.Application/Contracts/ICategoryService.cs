using GreenFood.Application.DTO;
using GreenFood.Application.DTO.OutputDto;

namespace GreenFood.Application.Contracts
{
    public interface ICategoryService
    {
        Task<Guid> CreateCategoryAsync(string categoryName);
        Task DeleteCategoryByIdAsync(Guid categoryId);
        Task<IEnumerable<OutputCategoryDto>> GetAllCategories();
    }
}
