using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.OutputDto;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Controllers
{
    [Route("Categories")]
    //[Authorize(Roles = $"{AccountRoles.GetAdministratorRole}")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _category;

        public CategoriesController(ICategoryService category)
        {
            _category = category;
        }

        [HttpGet]
        public async Task<IEnumerable<OutputCategoryDto>> GetAllCategories()
        {
            return await _category.GetAllCategories();
        }

        [HttpPost("{categoryName}")]
        public async Task<IActionResult> AddCategory([FromRoute] string categoryName)
        {
            var categoryId = await _category.CreateCategoryAsync(categoryName);

            return Ok(categoryId);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId)
        {
            await _category.DeleteCategoryByIdAsync(categoryId);

            return Ok(StatusCode(200));
        }
    }
}
