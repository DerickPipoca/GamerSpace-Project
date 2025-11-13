using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Categories.Queries
{
    public interface IGetAllCategoriesQuery
    {
        Task<IEnumerable<CategoryDto>> Execute();
    }
    public class GetAllCategoriesQuery : IGetAllCategoriesQuery
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetAllCategoriesQuery(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryDto>> Execute()
        {
            var categories = await _categoryRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
    }
}