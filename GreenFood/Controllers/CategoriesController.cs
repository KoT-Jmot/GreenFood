using FluentValidation;
using GreenFood.Application.Contracts;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.Validation;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GreenFood.Web.Controllers
{
    [Route("Categories")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _category;
        private readonly AddCategoryValidator _addRule;

        public CategoriesController(
            ICategoryService category,
            AddCategoryValidator addRule)
        {
            _category = category;
            _addRule = addRule;
        }

        [HttpGet]
        public async Task<IEnumerable<OutputCategoryDto>> GetAllCategories()
        {
            return await _category.GetAllCategoriesAsync();
        }

        [HttpGet("{categoryId}")]
        public async Task<OutputCategoryDto> GetCategoryById([FromRoute] Guid categoryId)
        {
            return await _category.GetCategoryByIdAsync(categoryId);
        }

        //[Authorize(Roles = $"{AccountRoles.GetAdministratorRole}")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto)
        {
            await _addRule.ValidateAndThrowAsync(categoryDto);

            var categoryId = await _category.CreateCategoryAsync(categoryDto);

            return Ok(categoryId);
        }

        //[Authorize(Roles = $"{AccountRoles.GetAdministratorRole}")]
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId)
        {
            await _category.DeleteCategoryByIdAsync(categoryId);

            return NoContent();
        }
    }
}
