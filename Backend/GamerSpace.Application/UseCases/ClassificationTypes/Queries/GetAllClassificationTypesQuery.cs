using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Entities;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.ClassificationTypes.Queries
{
    public interface IGetAllClassificationTypesQuery
    {
        Task<IEnumerable<ClassificationTypeDto>> Execute();
    }
    public class GetAllClassificationTypesQuery : IGetAllClassificationTypesQuery
    {
        private readonly IRepository<ClassificationType> _classificationTypeRepository;
        private readonly IMapper _mapper;

        public GetAllClassificationTypesQuery(IRepository<ClassificationType> classificationTypeRepository, IMapper mapper)
        {
            _classificationTypeRepository = classificationTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClassificationTypeDto>> Execute()
        {
            var classificationTypes = await _classificationTypeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClassificationTypeDto>>(classificationTypes);
        }
    }
}