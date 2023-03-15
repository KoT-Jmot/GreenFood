using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.RequestFeatures;
using GreenFood.Domain.Utils;
using GreenFood.Web.features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Controllers.UserControllers
{
    [Route("Categories")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryManager;

        public CategoriesController(ICategoryService category)
        {
            _categoryManager = category;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync(
            [FromQuery] CategoryQueryDto categoryQuery,
            CancellationToken cancellationToken)
        {
            var categoeies = await _categoryManager.GetAllCategoriesAsync(categoryQuery, cancellationToken);

            return new PagingActionResult<PagedList<OutputCategoryDto>>(categoeies);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryByIdAsync(
            [FromRoute] Guid categoryId,
            CancellationToken cancellationToken)
        {
            var category = await _categoryManager.GetCategoryByIdAsync(categoryId, cancellationToken);

            return Ok(category);
        }

        [Authorize(Roles = AccountRoles.GetAdministratorRole)]
        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync(
            [FromBody] CategoryDto categoryDto,
            CancellationToken cancellationToken)
        {
            var categoryId = await _categoryManager.CreateCategoryAsync(categoryDto, cancellationToken);

            return Created(nameof(CreateCategoryAsync), categoryId);
        }

        [Authorize(Roles = AccountRoles.GetAdministratorRole)]
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategoryAsync(
            [FromRoute] Guid categoryId,
            CancellationToken cancellationToken)
        {
            await _categoryManager.DeleteCategoryByIdAsync(categoryId, cancellationToken);

            return NoContent();
        }

        [Authorize(Roles = AccountRoles.GetAdministratorRole)]
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategoryAsync(
            [FromRoute] Guid categoryId,
            [FromBody] CategoryDto categoryDto,
            CancellationToken cancellationToken)
        {
            var updatingCategoryId = await _categoryManager.UpdateCategoryByIdAsync(categoryId, categoryDto, cancellationToken);

            return Created(nameof(UpdateCategoryAsync), updatingCategoryId);
        }
    }
}
