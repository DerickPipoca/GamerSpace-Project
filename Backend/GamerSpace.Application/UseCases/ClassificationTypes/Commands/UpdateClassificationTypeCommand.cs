using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Entities;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.ClassificationTypes.Commands
{
    public interface IUpdateClassificationTypeCommand
    {
        Task<ClassificationTypeDto> Execute(long classificationTypeId, UpdateClassificationTypeDto updateClassificationTypeDto);
    }
    public class UpdateClassificationTypeCommand : IUpdateClassificationTypeCommand
    {
        private readonly IRepository<ClassificationType> _classificationTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateClassificationTypeCommand(IRepository<ClassificationType> classificationTypeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _classificationTypeRepository = classificationTypeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ClassificationTypeDto> Execute(long classificationTypeId, UpdateClassificationTypeDto updateClassificationTypeDto)
        {
            var classificationType = await _classificationTypeRepository.GetByIdAsync(classificationTypeId);
            if (classificationType == null)
                throw new KeyNotFoundException($"ClassificationType with ID {classificationTypeId} not found.");

            classificationType.Update(updateClassificationTypeDto.Name);
            
            _classificationTypeRepository.Update(classificationType);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ClassificationTypeDto>(classificationType);
        }
    }
}