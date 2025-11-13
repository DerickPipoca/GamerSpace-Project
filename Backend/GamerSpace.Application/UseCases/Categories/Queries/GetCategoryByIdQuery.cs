using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Categories.Queries
{
    public interface IGetCategoryByIdQuery
    {
        Task<CategoryDto> Execute(long categoryId);
    }
    public class GetCategoryByIdQuery : IGetCategoryByIdQuery
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryByIdQuery(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<CategoryDto> Execute(long categoryId)
        {
            var category = await _categoryRepository.GetByIdWithDetailsAsync(categoryId);
            
            if (category == null)
                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");

            return _mapper.Map<CategoryDto>(category);
        }
    }
}