using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamerSpace.Application.DTOs;
using GamerSpace.Application.UseCases.Categories.Commands;
using GamerSpace.Application.UseCases.Categories.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamerSpace.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IGetAllCategoriesQuery _getAllCategoriesQuery;
        private readonly IGetCategoryByIdQuery _getCategoryByIdQuery;
        private readonly ICreateCategoryCommand _createCategoryCommand;
        private readonly IUpdateCategoryCommand _updateCategoryCommand;
        private readonly IDeleteCategoryCommand _deleteCategoryCommand;

        public CategoriesController(
            IGetAllCategoriesQuery getAllCategoriesQuery,
            IGetCategoryByIdQuery getCategoryByIdQuery,
            ICreateCategoryCommand createCategoryCommand,
            IUpdateCategoryCommand updateCategoryCommand,
            IDeleteCategoryCommand deleteCategoryCommand
        )
        {
            _getAllCategoriesQuery = getAllCategoriesQuery;
            _getCategoryByIdQuery = getCategoryByIdQuery;
            _createCategoryCommand = createCategoryCommand;
            _updateCategoryCommand = updateCategoryCommand;
            _deleteCategoryCommand = deleteCategoryCommand;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _getAllCategoriesQuery.Execute();
            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(long categoryId)
        {
            var category = await _getCategoryByIdQuery.Execute(categoryId);
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            var category = await _createCategoryCommand.Execute(createCategoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { categoryId = category.Id }, category);
        }

        [HttpPut("{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutCategory(long categoryId, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var category = await _updateCategoryCommand.Execute(categoryId, updateCategoryDto);
            return NoContent();
        }

        [HttpDelete("{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(long categoryId)
        {
            await _deleteCategoryCommand.Execute(categoryId);
            return NoContent();
        }
    }
}