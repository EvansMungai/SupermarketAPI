using Microsoft.AspNetCore.Authentication.Cookies;
using Supermarket.API.Extensions.ServiceHandlers;
using Supermarket.API.Features.Authentication.Services;
using Supermarket.API.Features.PaymentManagement.Models.Common;
using Supermarket.API.Features.PaymentManagement.Services;

namespace Supermarket.API.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        // Add Http Context Accessor
        services.AddHttpContextAccessor();

        // Add Authentication
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
        {
            options.LoginPath = "/auth/login";
            options.LogoutPath = "/auth/logout";
            options.AccessDeniedPath = "/auth/denied";
        });

        // Add Authorization
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy =>
                policy.RequireRole("Admin"));

            options.AddPolicy("CustomerOnly", policy =>
                policy.RequireRole("Customer"));

            options.AddPolicy("AdminOrCustomer", policy =>
                policy.RequireRole("Admin", "Customer"));
        });

        // Register Authentication Services
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        // Bind MpesaConfig from configuration
        services.Configure<MpesaConfig>(config.GetSection("MpesaConfig"));

        // Register HTTP Client
        services.AddHttpClient<IMpesaApiService, MpesaApiService>();

        // Register Memory Cache
        services.AddMemoryCache();

        // Configure Cors
        services.ConfigureCors();

        // Register application services via reflection
        services.RegisterFeatureServices();

        return services;
    }
}

