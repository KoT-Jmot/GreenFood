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

        public CategoriesController(ICategoryService category)
        {
            _category = category;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var categories = await _category.GetAllCategoriesAsync();

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] Guid categoryId)
        { 
            var category = await _category.GetCategoryByIdAsync(categoryId);

            return Ok(category);
        }

        //[Authorize(Roles = $"{AccountRoles.GetAdministratorRole}")]
        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryDto categoryDto)
        {
            var categoryId = await _category.CreateCategoryAsync(categoryDto);

            return Created(nameof(CreateCategoryAsync), categoryId);
        }

        //[Authorize(Roles = $"{AccountRoles.GetAdministratorRole}")]
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] Guid categoryId)
        {
            await _category.DeleteCategoryByIdAsync(categoryId);

            return NoContent();
        }
    }
}
