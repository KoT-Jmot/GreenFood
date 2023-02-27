using FluentValidation;
using GreenFood.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFood.Application.Validation
{
    public class AddProductValidation : AbstractValidator<ProductForAddDto>
    {
        public AddProductValidation()
        {
            RuleFor(p=>p.Header)
                .NotEmpty()
                .NotNull()
                .MinimumLength(5)
                .MaximumLength(50)
                .WithMessage("Enter correct Header!");

            RuleFor(p => p.Description)
                .MaximumLength(300)
                .WithMessage("Enter correct description!");

            RuleFor(p => p.CategoryId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Enter correct type of product!");

            RuleFor(p => p.Price)
                .NotNull()
                .NotEmpty()
                .WithMessage("Enter correct price!");

            RuleFor(p => p.Count)
                .NotEmpty()
                .NotNull()
                .WithMessage("Enter correct count!");
        }
    }
}
