using FluentValidation;
using GreenFood.Application.DTO;

namespace GreenFood.Application.Validation
{
    public class LoginUserValidator : AbstractValidator<UserForLoginDto>
    {
        public LoginUserValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Enter correct email");

            RuleFor(u => u.Password)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50)
                .WithMessage("Enter correct password");
        }
    }
}