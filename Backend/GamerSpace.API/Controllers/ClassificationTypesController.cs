using GamerSpace.Application.DTOs;
using GamerSpace.Application.UseCases.ClassificationTypes.Commands;
using GamerSpace.Application.UseCases.ClassificationTypes.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamerSpace.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet]
        public async Task<IActionResult> GetAllClassificationTypes()
        {
            var classificationTypes = await _getAllClassificationTypesQuery.Execute();
            return Ok(classificationTypes);
        }

        [HttpGet("{classificationTypeId}")]
        public async Task<IActionResult> GetClassificationTypeById(long classificationTypeId)
        {
            var classificationTypes = await _getClassificationTypeByIdQuery.Execute(classificationTypeId);
            return Ok(classificationTypes);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostClassificationType([FromBody] CreateClassificationTypeDto createClassificationTypeDto)
        {
            var classificationType = await _createClassificationTypeCommand.Execute(createClassificationTypeDto);
            return CreatedAtAction(nameof(GetClassificationTypeById), new { classificationTypeId = classificationType.Id }, classificationType);
        }

        [HttpPut("{classificationTypeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutClassicationType(long classificationTypeId, [FromBody] UpdateClassificationTypeDto updateClassificationTypeDto)
        {
            var classificationType = await _updateClassificationTypeCommand.Execute(classificationTypeId, updateClassificationTypeDto);
            return NoContent();
        }

        [HttpDelete("{classificationTypeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteClassificationType(long classificationTypeId)
        {
            await _deleteClassificationTypeCommand.Execute(classificationTypeId);
            return NoContent();
        }
    }
}