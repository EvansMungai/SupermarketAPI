using Microsoft.AspNetCore.Mvc;
using Supermarket.API.Features.DrinkManagement.Models;
using Supermarket.API.Features.DrinkManagement.Services;

namespace Supermarket.API.Features.DrinkManagement.Endpoints;

public static class DrinkEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/drinks")
                       .WithTags("Drinks");

        group.MapGet("/", async (DrinkService service) =>
            await service.GetAllDrinksAsync());

        group.MapGet("/{id}", async (int id, DrinkService service) =>
            await service.GetDrinkByIdAsync(id));

        group.MapPost("/", async (Drink drink, DrinkService service) =>
            await service.CreateDrinkAsync(drink));

        group.MapPut("/{id}", async (int id, Drink drink, DrinkService service) =>
        {
            if (id != drink.DrinkId) return Results.BadRequest("Drink ID mismatch");
            return await service.UpdateDrinkAsync(drink);
        });

        group.MapDelete("/{id}", async (int id, DrinkService service) =>
            await service.DeleteDrinkAsync(id));
    }
}
