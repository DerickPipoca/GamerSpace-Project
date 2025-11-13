using FluentValidation;
using GamerSpace.Application.DTOs;

namespace GamerSpace.Application.Validators.Product
{
    public class UpdateProductCategoriesDtoValidator : AbstractValidator<UpdateProductCategoriesDto>
    {
        public UpdateProductCategoriesDtoValidator()
        {
            RuleForEach(v => v.CategoryIds)
                .GreaterThan(0).WithMessage("Category id must be a valid number");
        }
    }
}