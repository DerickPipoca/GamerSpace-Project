using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Categories.Commands
{
    public interface IUpdateCategoryCommand
    {
        Task<CategoryDto> Execute(long categoryId, UpdateCategoryDto updateCategoryDto);
    }
    public class UpdateCategoryCommand : IUpdateCategoryCommand
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCategoryCommand(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CategoryDto> Execute(long categoryId, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            
            if (category == null)
                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");

            category.Update(updateCategoryDto.Name);
            _categoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CategoryDto>(category);
        }
    }
}