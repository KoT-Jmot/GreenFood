using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Controllers.AdminControllers
{
    [Route("Admin/Categories")]
    [Authorize(Roles = AccountRoles.GetAdministratorRole)]
    public class CategoriesAdminController : Controller
    {
        private readonly ICategoryService _category;

        public CategoriesAdminController(ICategoryService category)
        {
            _category = category;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync(
            [FromBody] CategoryDto categoryDto,
            CancellationToken cancellationToken)
        {
            var categoryId = await _category.CreateCategoryAsync(categoryDto, cancellationToken);

            return Created(nameof(CreateCategoryAsync), categoryId);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategoryAsync(
            [FromRoute] Guid categoryId,
            CancellationToken cancellationToken)
        {
            await _category.DeleteCategoryByIdAsync(categoryId, cancellationToken);

            return NoContent();
        }
    }
}
