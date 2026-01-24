using FluentValidation;
using Supermarket.API.Features.BranchManagement.Models;

namespace Supermarket.API.Features.BranchManagement.Validators;

public class BranchValidator : AbstractValidator<Branch>
{
    public BranchValidator()
    {
        RuleFor(b => b.BranchName)
            .NotEmpty().WithMessage("Branch name is required.")
            .MaximumLength(50).WithMessage("Branch name must not exceed 50 characters.");

        RuleFor(b => b.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MaximumLength(50).WithMessage("Location must not exceed 50 characters.");
    }
}
