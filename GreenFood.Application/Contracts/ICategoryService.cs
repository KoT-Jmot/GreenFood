using GreenFood.Application.DTO;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;

namespace GreenFood.Application.Contracts
{
    public interface ICategoryService
    {
        Task<PagedList<OutputCategoryDto>> GetAllCategoriesAsync(
            CategoryQueryDto categoryQuery,
            CancellationToken cancellationToken);
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
