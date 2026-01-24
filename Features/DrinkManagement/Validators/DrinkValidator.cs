using FluentValidation;
using Supermarket.API.Features.DrinkManagement.Models;

namespace Supermarket.API.Features.DrinkManagement.Validators;

public class DrinkValidator : AbstractValidator<Drink>
{
    public DrinkValidator()
    {
        RuleFor(d => d.DrinkName)
            .NotEmpty().WithMessage("Drink name is required.")
            .MaximumLength(30).WithMessage("Drink name must not exceed 30 characters.");

        RuleFor(d => d.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}
