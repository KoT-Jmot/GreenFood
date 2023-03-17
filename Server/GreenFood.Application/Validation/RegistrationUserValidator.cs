using FluentValidation;
using GreenFood.Application.DTO.InputDto.UserDto;

namespace GreenFood.Application.Validation
{
    public class RegistrationUserValidator : AbstractValidator<UserForRegistrationDto>
    {
        public RegistrationUserValidator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(30)
                .WithMessage("Enter correct name");

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

            RuleFor(u => u.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .Matches("[+]{1}[0-9]{10,12}")
                .WithMessage("Enter correct phone number");

            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithMessage("Enter correct email");
        }
    }
}