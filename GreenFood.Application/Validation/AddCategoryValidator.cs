using FluentValidation;
using GreenFood.Application.DTO.InputDto;

namespace GreenFood.Application.Validation
{
    public class AddCategoryValidator : AbstractValidator<CategoryDto>
    {
        public AddCategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(20)
                .WithMessage("Incorrect category name!");
        }
    }
}
