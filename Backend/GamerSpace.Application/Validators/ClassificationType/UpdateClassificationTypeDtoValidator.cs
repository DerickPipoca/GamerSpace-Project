using FluentValidation;
using GamerSpace.Application.DTOs;

namespace GamerSpace.Application.Validators.ClassificationType
{
    public class UpdateClassificationTypeDtoValidator : AbstractValidator<UpdateClassificationTypeDto>
    {
        public UpdateClassificationTypeDtoValidator()
        {
            RuleFor(v => v.Name)
                .MinimumLength(3).WithMessage("Category name must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("Category name cannot exceed 50 characters.");
        }
    }
}