using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Entities;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Categories.Commands
{
    public interface ICreateCategoryCommand
    {
        Task<CategoryDto> Execute(CreateCategoryDto createCategoryDto);
    }
    public class CreateCategoryCommand : ICreateCategoryCommand
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRepository<ClassificationType> _classificationTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategoryCommand(ICategoryRepository categoryRepository, IRepository<ClassificationType> classificationTypeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _classificationTypeRepository = classificationTypeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Execute(CreateCategoryDto createCategoryDto)
        {
            var classificationType = await _classificationTypeRepository.GetByIdAsync(createCategoryDto.TypeId);
            if (classificationType == null)
                throw new KeyNotFoundException($"ClassificationType with ID {createCategoryDto.TypeId} not found.");


            var category = new Category(createCategoryDto.Name, createCategoryDto.TypeId);

            await _categoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            var createdCategoryWithDetails = await _categoryRepository.GetByIdWithDetailsAsync(category.Id);

            return _mapper.Map<CategoryDto>(createdCategoryWithDetails);
        }
    }
}