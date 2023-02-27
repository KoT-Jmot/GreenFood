using FluentValidation;
using GreenFood.Application.DTO;

namespace GreenFood.Application.Validation
{
    public class AddCategoryValidator : AbstractValidator<CategoryForAddDto>
    {
        public AddCategoryValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(5)
                .MaximumLength(30);
        }
    }
}
