using FluentValidation;
using GreenFood.Application.DTO.ServicesDto;

namespace GreenFood.Application.Validation
{
    public class LoginUserValidator : AbstractValidator<UserForLoginDto>
    {
        public LoginUserValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithMessage("Enter correct email");

            RuleFor(u => u.Password)
                .NotEmpty()
                .NotNull()
                .Matches("[a-z]+")
                .Matches("[A-Z]+")
                .Matches("[0-9]+")
                .Matches("[\\W]+")
                .MinimumLength(8)
                .MaximumLength(50)
                .WithMessage("Enter correct password");
        }
    }
}