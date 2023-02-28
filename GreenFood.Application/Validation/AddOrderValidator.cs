using FluentValidation;
using GreenFood.Application.DTO;

namespace GreenFood.Application.Validation
{
    public class AddOrderValidator : AbstractValidator<OrderForAddDto>
    {
        public AddOrderValidator()
        {
            RuleFor(o => o.ProductId)
                .NotNull()
                .NotEmpty();

            RuleFor(o => o.Count)
                .GreaterThan(0)
                .LessThan(10000000);
        }
    }
}
