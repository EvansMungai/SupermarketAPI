using FluentValidation;
using Supermarket.API.Features.SalesManagement.Models;

namespace Supermarket.API.Features.SalesManagement.Validators;

public class SaleValidator : AbstractValidator<SaleRequest>
{
    public SaleValidator()
    {
        RuleFor(s => s.BranchId).GreaterThan(0);
        RuleFor(s => s.DrinkId).GreaterThan(0);
        RuleFor(s => s.Quantity).GreaterThan(0);
    }
}
