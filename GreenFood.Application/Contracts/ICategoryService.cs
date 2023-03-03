using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;

namespace GreenFood.Application.Contracts
{
    public interface ICategoryService
    {
        Task<OutputCategoryDto> GetCategoryByIdAsync(Guid categoryId);
        Task<IEnumerable<OutputCategoryDto>> GetAllCategoriesAsync();
        Task<Guid> CreateCategoryAsync(CategoryDto categoryDto);
        Task DeleteCategoryByIdAsync(Guid categoryId);
    }
}
