using GamerSpace.Application.DTOs;
using GamerSpace.Application.UseCases.ClassificationTypes.Commands;
using GamerSpace.Application.UseCases.ClassificationTypes.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamerSpace.API.Controllers
{
    /// <summary>
    /// Gerencia os tipos de classificação do sistema (ex: Cor, Especificação, Departamento).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ClassificationTypesController : ControllerBase
    {
        private readonly ICreateClassificationTypeCommand _createClassificationTypeCommand;
        private readonly IGetAllClassificationTypesQuery _getAllClassificationTypesQuery;
        private readonly IGetClassificationTypeByIdQuery _getClassificationTypeByIdQuery;
        private readonly IUpdateClassificationTypeCommand _updateClassificationTypeCommand;
        private readonly IDeleteClassificationTypeCommand _deleteClassificationTypeCommand;

        public ClassificationTypesController(
            ICreateClassificationTypeCommand createClassificationTypeCommand,
            IGetAllClassificationTypesQuery getAllClassificationTypesQuery,
            IGetClassificationTypeByIdQuery getClassificationTypeByIdQuery,
            IUpdateClassificationTypeCommand updateClassificationTypeCommand,
            IDeleteClassificationTypeCommand deleteClassificationTypeCommand)
        {
            _createClassificationTypeCommand = createClassificationTypeCommand;
            _getAllClassificationTypesQuery = getAllClassificationTypesQuery;
            _getClassificationTypeByIdQuery = getClassificationTypeByIdQuery;
            _updateClassificationTypeCommand = updateClassificationTypeCommand;
            _deleteClassificationTypeCommand = deleteClassificationTypeCommand;
        }

        /// <summary>
        /// Lista todos os tipos de classificação disponíveis.
        /// </summary>
        /// <returns>Uma lista contendo todos os tipos de classificação.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllClassificationTypes()
        {
            var classificationTypes = await _getAllClassificationTypesQuery.Execute();
            return Ok(classificationTypes);
        }

        /// <summary>
        /// Obtém os detalhes de um tipo de classificação específico pelo seu Id.
        /// </summary>
        /// <param name="classificationTypeId">Id único do tipo de classificação.</param>
        /// <returns>Os detalhes do tipo de classificação.</returns>
        [HttpGet("{classificationTypeId}")]
        public async Task<IActionResult> GetClassificationTypeById(long classificationTypeId)
        {
            var classificationTypes = await _getClassificationTypeByIdQuery.Execute(classificationTypeId);
            return Ok(classificationTypes);
        }

        /// <summary>
        /// Cria um novo tipo de classificação.
        /// </summary>
        /// <param name="createClassificationTypeDto">Dados necessários para criar a classificação.</param>
        /// <returns>O tipo de classificação recém-criado.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostClassificationType([FromBody] CreateClassificationTypeDto createClassificationTypeDto)
        {
            var classificationType = await _createClassificationTypeCommand.Execute(createClassificationTypeDto);
            return CreatedAtAction(nameof(GetClassificationTypeById), new { classificationTypeId = classificationType.Id }, classificationType);
        }

        /// <summary>
        /// Atualiza os dados de um tipo de classificação existente.
        /// </summary>
        /// <param name="classificationTypeId">Id do tipo de classificação a ser atualizado.</param>
        /// <param name="updateClassificationTypeDto">Novos dados da classificação.</param>
        /// <returns>N/A.</returns>
        [HttpPut("{classificationTypeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutClassicationType(long classificationTypeId, [FromBody] UpdateClassificationTypeDto updateClassificationTypeDto)
        {
            var classificationType = await _updateClassificationTypeCommand.Execute(classificationTypeId, updateClassificationTypeDto);
            return NoContent();
        }

        /// <summary>
        /// Remove um tipo de classificação do sistema.
        /// </summary>
        /// <param name="classificationTypeId">Id do tipo de classificação a ser removido.</param>
        /// <returns>N/A.</returns>
        [HttpDelete("{classificationTypeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteClassificationType(long classificationTypeId)
        {
            await _deleteClassificationTypeCommand.Execute(classificationTypeId);
            return NoContent();
        }
    }
}