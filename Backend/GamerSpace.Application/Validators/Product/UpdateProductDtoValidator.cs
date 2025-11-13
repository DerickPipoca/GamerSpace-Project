using FluentValidation;
using GamerSpace.Application.DTOs;

namespace GamerSpace.Application.Validators.Product
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(v => v.Name)
                .MinimumLength(3).WithMessage("Product name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(v => v.Description)
                .MaximumLength(1200).WithMessage("Description cannot exceed 1200 characters.");
        }
    }
}