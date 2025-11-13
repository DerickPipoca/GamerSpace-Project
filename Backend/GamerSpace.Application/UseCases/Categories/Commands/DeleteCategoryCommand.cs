using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Categories.Commands
{
    public interface IDeleteCategoryCommand
    {
        Task Execute(long categoryId);
    }
    public class DeleteCategoryCommand : IDeleteCategoryCommand
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteCategoryCommand(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if (category == null)
                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");

            category.Disable();
            await _unitOfWork.SaveChangesAsync();
        }
    }
}