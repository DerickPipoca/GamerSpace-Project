using FluentValidation;
using GamerSpace.Application.DTOs;

namespace GamerSpace.Application.Validators.Product
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MinimumLength(3).WithMessage("Product name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(v => v.Description)
                .MaximumLength(1200).WithMessage("Description cannot exceed 1200 characters.");

            RuleFor(v => v.Variants)
                .NotEmpty().WithMessage("A product must have at least one variant.");

            RuleForEach(v => v.Variants)
                .SetValidator(new CreateProductVariantDtoValidator());

        }
    }
}