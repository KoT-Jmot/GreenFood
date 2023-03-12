using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Controllers.AdminControllers
{
    [Authorize(Roles = AccountRoles.GetAdministratorRole)]
    [Route("Admin/Categories")]
    public class CategoriesAdminController : Controller
    {
        private readonly ICategoryService _categoryManager;

        public CategoriesAdminController(ICategoryService categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync(
            [FromBody] CategoryDto categoryDto,
            CancellationToken cancellationToken)
        {
            var categoryId = await _categoryManager.CreateCategoryAsync(categoryDto, cancellationToken);

            return Created(nameof(CreateCategoryAsync), categoryId);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategoryAsync(
            [FromRoute] Guid categoryId,
            CancellationToken cancellationToken)
        {
            await _categoryManager.DeleteCategoryByIdAsync(categoryId, cancellationToken);

            return NoContent();
        }
    }
}
