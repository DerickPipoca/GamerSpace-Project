using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Entities;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.ClassificationTypes.Queries
{
    public interface IGetClassificationTypeByIdQuery
    {
        Task<ClassificationTypeDto> Execute(long classificationTypeId);
    }

    public class GetClassificationTypeByIdQuery : IGetClassificationTypeByIdQuery
    {

        private readonly IRepository<ClassificationType> _classificationTypeRepository;
        private readonly IMapper _mapper;

        public GetClassificationTypeByIdQuery(IRepository<ClassificationType> classificationTypeRepository, IMapper mapper)
        {
            _classificationTypeRepository = classificationTypeRepository;
            _mapper = mapper;
        }
        public async Task<ClassificationTypeDto> Execute(long classificationTypeId)
        {
            var classificationType = await _classificationTypeRepository.GetByIdAsync(classificationTypeId);
            if (classificationType == null)
            {
                throw new KeyNotFoundException($"ClassificationType with ID {classificationTypeId} not found.");
            }
            return _mapper.Map<ClassificationTypeDto>(classificationType);
        }
    }
}