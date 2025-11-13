using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Entities;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.ClassificationTypes.Commands
{
    public interface ICreateClassificationTypeCommand
    {
        Task<ClassificationTypeDto> Execute(CreateClassificationTypeDto createCategoryDto);
    }
    public class CreateClassificationTypeCommand : ICreateClassificationTypeCommand
    {
        private readonly IRepository<ClassificationType> _classificationTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateClassificationTypeCommand(IRepository<ClassificationType> classificationTypeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _classificationTypeRepository = classificationTypeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ClassificationTypeDto> Execute(CreateClassificationTypeDto createCategoryDto)
        {
            var classificationType = new ClassificationType(createCategoryDto.Name);
            await _classificationTypeRepository.AddAsync(classificationType);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ClassificationTypeDto>(classificationType);
        }
    }
}