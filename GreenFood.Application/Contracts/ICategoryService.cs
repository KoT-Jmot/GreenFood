using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;

namespace GreenFood.Application.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<OutputCategoryDto>> GetAllCategoriesAsync(CancellationToken cancellationToken);
        Task<OutputCategoryDto> GetCategoryByIdAsync(
            Guid categoryId,
            CancellationToken cancellationToken);
        Task<Guid> CreateCategoryAsync(
            CategoryDto categoryDto,
            CancellationToken cancellationToken);
        Task DeleteCategoryByIdAsync(
            Guid categoryId,
            CancellationToken cancellationToken);
    }
}
