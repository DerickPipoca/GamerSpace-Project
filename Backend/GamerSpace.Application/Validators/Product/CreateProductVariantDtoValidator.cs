using FluentValidation;
using GamerSpace.Application.DTOs;

namespace GamerSpace.Application.Validators.Product
{
    public class CreateProductVariantDtoValidator : AbstractValidator<CreateProductVariantDto>
    {
        public CreateProductVariantDtoValidator()
        {
            RuleFor(v => v.SKU)
                .NotEmpty().WithMessage("SKU is required")
                .MaximumLength(50).WithMessage("SKU cannot exceed 50 characters.");

            RuleFor(v => v.Description)
                .MaximumLength(1200).WithMessage("Description cannot exceed 1200 characters.");

            RuleFor(v => v.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(v => v.StockAmount)
                .GreaterThanOrEqualTo((short)0).WithMessage("Stock amount cannot be negative.");
        }
    }
}