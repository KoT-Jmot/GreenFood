using FluentValidation;
using GreenFood.Application.Contracts;
using GreenFood.Application.DTO;
using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.Validation;
using GreenFood.Web.features;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<CustomActionResult<PagedList<OutputCategoryDto>>> GetAllCategoriesAsync(
            [FromQuery] CategoryQueryDto categoryQuery,
            CancellationToken cancellationToken)
        {
            var result = await _category.GetAllCategoriesAsync(categoryQuery, cancellationToken);

            return new CustomActionResult<PagedList<OutputCategoryDto>>(result);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryByIdAsync(
            [FromRoute] Guid categoryId,
            CancellationToken cancellationToken)
        { 
            var category = await _category.GetCategoryByIdAsync(categoryId, cancellationToken);

            return Ok(category);
        }

        //[Authorize(Roles = $"{AccountRoles.GetAdministratorRole}")]
        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync(
            [FromBody] CategoryDto categoryDto,
            CancellationToken cancellationToken)
        {
            var categoryId = await _category.CreateCategoryAsync(categoryDto, cancellationToken);

            return Created(nameof(CreateCategoryAsync), categoryId);
        }

        //[Authorize(Roles = $"{AccountRoles.GetAdministratorRole}")]
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
