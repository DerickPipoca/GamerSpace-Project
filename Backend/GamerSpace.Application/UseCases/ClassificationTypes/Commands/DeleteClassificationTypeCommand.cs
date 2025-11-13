using GamerSpace.Domain.Entities;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.ClassificationTypes.Commands
{
    public interface IDeleteClassificationTypeCommand
    {
        Task Execute(long classificationTypeId);
    }
    public class DeleteClassificationTypeCommand : IDeleteClassificationTypeCommand
    {
        private readonly IRepository<ClassificationType> _classificationTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClassificationTypeCommand(IRepository<ClassificationType> classificationTypeRepository, IUnitOfWork unitOfWork)
        {
            _classificationTypeRepository = classificationTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long classificationTypeId)
        {
            var classificationType = await _classificationTypeRepository.GetByIdAsync(classificationTypeId);
            if (classificationType == null)
                throw new KeyNotFoundException($"ClassificationType with ID {classificationTypeId} not found.");

            foreach (var categories in classificationType.Categories)
            {
                categories.Disable();
            }
            classificationType.Disable();
            await _unitOfWork.SaveChangesAsync();
        }
    }
}