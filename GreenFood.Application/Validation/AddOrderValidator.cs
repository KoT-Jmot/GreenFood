﻿using FluentValidation;
using GreenFood.Application.DTO.InputDto;

namespace GreenFood.Application.Validation
{
    public class AddOrderValidator : AbstractValidator<InputOrderDto>
    {
        public AddOrderValidator()
        {
            RuleFor(o => o.ProductId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Incorrect product!");

            RuleFor(o => o.Count)
                .GreaterThan(0)
                .LessThan(10000000)
                .WithMessage("Incorrect count!");
        }
    }
}
