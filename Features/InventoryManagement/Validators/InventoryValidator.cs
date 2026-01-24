using FluentValidation;
using Supermarket.API.Features.InventoryManagement.Models;

namespace Supermarket.API.Features.InventoryManagement.Validators;

public class InventoryValidator : AbstractValidator<Inventory>
{
    public InventoryValidator()
    {
        RuleFor(x => x.StockQuantity).GreaterThanOrEqualTo(0);
    }
}

public class RestockRequestValidator : AbstractValidator<RestockRequest>
{
    public RestockRequestValidator()
    {
        RuleFor(x => x.BranchId).GreaterThan(0);
        RuleFor(x => x.DrinkId).GreaterThan(0);
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}
