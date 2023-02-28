using GreenFood.Application.Contracts;
using GreenFood.Application.DTO;
using GreenFood.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Controllers
{
    [Route("Categories")]
    [Authorize(Roles = $"{AccountRoles.GetAdministratorRole}")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _category;

        public CategoriesController(ICategoryService category)
        {
            _category = category;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromRoute] string categoryName)
        {
            await _category.CreateCategoryAsync(categoryName);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId)
        {
            await _category.DeleteCategoryAsync(categoryId);

            return Ok();
        }
    }
}
