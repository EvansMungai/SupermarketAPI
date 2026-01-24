using Supermarket.API.Features.DrinkManagement.Models;
using Supermarket.API.Features.DrinkManagement.Services.Repositories;

namespace Supermarket.API.Features.DrinkManagement.Services;

public class DrinkService
{
    private readonly IDrinkRepository _repository;

    public DrinkService(IDrinkRepository repository)
    {
        _repository = repository;
    }

    public async Task<IResult> GetAllDrinksAsync()
    {
        IEnumerable<Drink> drinks = await _repository.GetAllAsync();
        return drinks == null || !drinks.Any() ? Results.NotFound("No drinks found") : Results.Ok(drinks);
    }

    public async Task<IResult> GetDrinkByIdAsync(int id)
    {
        Drink? drink = await _repository.GetByIdAsync(id);
        return drink == null ? Results.NotFound($"Drink with ID = {id} was not found") : Results.Ok(drink);
    }

    public async Task<IResult> CreateDrinkAsync(Drink drink)
    {
        try
        {
            await _repository.AddAsync(drink);
            return Results.Created($"/api/drinks/{drink.DrinkId}", drink);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.InnerException?.Message ?? ex.Message);
        }
    }

    public async Task<IResult> UpdateDrinkAsync(Drink drink)
    {
        try
        {
            Drink? existing = await _repository.GetByIdAsync(drink.DrinkId);
            if (existing == null) return Results.NotFound($"Drink with ID = {drink.DrinkId} was not found");

            await _repository.UpdateAsync(drink);
            Drink? updated = await _repository.GetByIdAsync(drink.DrinkId);
            return Results.Ok(updated);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.InnerException?.Message ?? ex.Message);
        }
    }

    public async Task<IResult> DeleteDrinkAsync(int id)
    {
        try
        {
            Drink? existing = await _repository.GetByIdAsync(id);
            if (existing == null) return Results.NotFound($"Drink with ID = {id} was not found");

            await _repository.DeleteAsync(id);
            return Results.Ok(existing);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.InnerException?.Message ?? ex.Message);
        }
    }
}
