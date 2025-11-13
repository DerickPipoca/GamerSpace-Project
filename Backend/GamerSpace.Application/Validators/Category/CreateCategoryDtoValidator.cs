using FluentValidation;
using GamerSpace.Application.DTOs;

namespace GamerSpace.Application.Validators.Category
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MinimumLength(3).WithMessage("Category name must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("Category name cannot exceed 50 characters.");

            RuleFor(v => v.TypeId)
                .GreaterThan(0).WithMessage("Category type id must be a valid number");
        }
    }
}