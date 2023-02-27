using GreenFood.Application.Contracts;
using GreenFood.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Controllers
{
    [Route("Category")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _category;

        public CategoryController(ICategoryService category)
        {
            _category = category;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryForAddDto categoryDto)
        {
            await _category.CreateCategoryAsync(categoryDto);

            return Ok();
        }
    }
}
