using GamerSpace.Application.DTOs;
using GamerSpace.Application.UseCases.Categories.Commands;
using GamerSpace.Application.UseCases.Categories.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamerSpace.API.Controllers
{
    /// <summary>
    /// Gerencia as categorias de produtos da plataforma (ex: Preto, Azul, Sem fio, Headset).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
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

        /// <summary>
        /// Lista todas as categorias disponíveis no sistema.
        /// </summary>
        /// <returns>Uma lista contendo todas as categorias.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _getAllCategoriesQuery.Execute();
            return Ok(categories);
        }

        /// <summary>
        /// Obtém os detalhes de uma categoria específica pelo seu Id.
        /// </summary>
        /// <param name="categoryId">Id único da categoria.</param>
        /// <returns>Os detalhes da categoria solicitada.</returns>
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(long categoryId)
        {
            var category = await _getCategoryByIdQuery.Execute(categoryId);
            return Ok(category);
        }

        /// <summary>
        /// Cria uma nova categoria.
        /// </summary>
        /// <param name="createCategoryDto">Dados necessários para criar a categoria.</param>
        /// <returns>A categoria recém-criada.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            var category = await _createCategoryCommand.Execute(createCategoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { categoryId = category.Id }, category);
        }

        /// <summary>
        /// Atualiza os dados de uma categoria existente.
        /// </summary>
        /// <param name="categoryId">Id da categoria a ser atualizada.</param>
        /// <param name="updateCategoryDto">Novos dados da categoria.</param>
        /// <returns>N/A.</returns>
        [HttpPut("{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutCategory(long categoryId, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var category = await _updateCategoryCommand.Execute(categoryId, updateCategoryDto);
            return NoContent();
        }

        /// <summary>
        /// Remove uma categoria do sistema.
        /// </summary>
        /// <param name="categoryId">Id da categoria a ser removida.</param>
        /// <returns>N/A.</returns>
        [HttpDelete("{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(long categoryId)
        {
            await _deleteCategoryCommand.Execute(categoryId);
            return NoContent();
        }
    }
}