using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;

namespace GreenFood.Application.Contracts
{
    public interface ICategoryService
    {
        Task<OutputCategoryDto> GetCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<IEnumerable<OutputCategoryDto>> GetAllCategoriesAsync(CancellationToken cancellationToken = default);
        Task<Guid> CreateCategoryAsync(CategoryDto categoryDto, CancellationToken cancellationToken = default);
        Task DeleteCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
    }
}
