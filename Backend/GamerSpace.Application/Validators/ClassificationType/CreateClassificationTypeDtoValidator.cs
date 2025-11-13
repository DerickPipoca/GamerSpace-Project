using FluentValidation;
using GamerSpace.Application.DTOs;

namespace GamerSpace.Application.Validators.ClassificationType
{
    public class CreateClassificationTypeDtoValidator : AbstractValidator<CreateClassificationTypeDto>
    {
        public CreateClassificationTypeDtoValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Category Type name is required.")
                .MinimumLength(3).WithMessage("Category Type name must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("Category Type name cannot exceed 50 characters.");
        }
    }
}