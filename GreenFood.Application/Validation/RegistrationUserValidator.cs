using FluentValidation;
using GreenFood.Application.DTO;

namespace GreenFood.Application.Validation
{
    public class RegistrationUserValidator : AbstractValidator<UserForRegistrationDto>
    {
        public RegistrationUserValidator()
        {
            RuleFor(u => u.FullName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(30)
                .WithMessage("Enter correct name");

            RuleFor(u => u.Password)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50)
                .WithMessage("Enter correct password");

            RuleFor(u => u.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .Length(13)
                .WithMessage("Enter correct phone number");

            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Enter correct email");
        }
    }
}