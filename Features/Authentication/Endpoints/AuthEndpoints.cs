using Microsoft.AspNetCore.Mvc;
using Supermarket.API.Features.Authentication.Models;
using Supermarket.API.Features.Authentication.Services;
using System.Security.Claims;

namespace Supermarket.API.Features.Authentication.Endpoints;

public static class AuthEndpoints
{
    public static void Map(this WebApplication app)
    {
        var authGroup = app.MapGroup("/auth")
            .WithTags("Authentication");

        authGroup.MapPost("/login", async ([FromServices] IAuthenticationService authService,
                                           [FromBody] CreateUserRequest request) =>
        {
            var username = request.Username;
            var password = request.Password;

            var result = await authService.LoginAsync(username, password);
            if (result == null) return Results.Unauthorized();

            return Results.Ok(new { message = "Logged in", user = result });
        });

        authGroup.MapPost("/logout", async ([FromServices] IAuthenticationService authService) =>
        {
            await authService.LogoutAsync();
            return Results.Ok(new { message = "Logged out" });
        }).RequireAuthorization(); 


        authGroup.MapPost("/create-default-user", async ([FromServices] IAuthenticationService authService,
                                                         [FromBody] CreateUserRequest request) =>
        {
            try
            {
                var user = await authService.CreateUserAsync(request.Username, request.Password, "Customer");
                return Results.Ok(new { message = "Customer user created", user = user.UserName });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        });

        authGroup.MapPost("/create-admin-user", async ([FromServices] IAuthenticationService authService,
                                                       ClaimsPrincipal user,
                                                       [FromBody] CreateUserRequest request) =>
        {
            // Extra check: current user must be Admin
            //if (!user.IsInRole("Admin"))
            //    return Results.Forbid();

            try
            {
                var newAdmin = await authService.CreateUserAsync(request.Username, request.Password, "Admin");
                return Results.Ok(new { message = "Admin user created", user = newAdmin.UserName });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        });
    }
}
