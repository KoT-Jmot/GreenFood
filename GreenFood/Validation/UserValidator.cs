using FluentValidation;
using GreenFood.Domain.Models;
using GreenFood.Web.TDOModels;

namespace GreenFood.Web.Validation
{
    public class UserValidator : AbstractValidator<UserForRegistrationDto>
    {
        public UserValidator()
        {
            RuleFor(u => new { u.FullName, u.Password, u.Email, u.PhoneNumber }).NotEmpty().NotNull().WithMessage("Enter correct data");
            RuleFor(u => u.FullName).MaximumLength(30).WithMessage("Maximum size of name - 30");
            RuleFor(u => u.Password).MaximumLength(50).WithMessage("Maximum size of password - 30");
            RuleFor(u => u.PhoneNumber).Length(13).WithMessage("Enter correct phone number");
        }
    }
}