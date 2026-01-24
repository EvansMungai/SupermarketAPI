using FluentValidation;
using FluentValidation.AspNetCore;

namespace Supermarket.API.Extensions.ValidationHandlers;

public static class FluentValidationRegistration
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<Program>();
        return services;
    }
}
