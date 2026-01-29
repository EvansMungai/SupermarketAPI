using Supermarket.API.Features.InventoryManagement.Models;
using Supermarket.API.Features.InventoryManagement.Services.Repositories;

namespace Supermarket.API.Features.InventoryManagement.Services;

public class InventoryService
{
    private readonly IInventoryRepository _repository;

    public InventoryService(IInventoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IResult> RestockAsync(int branchId, int drinkId, int quantity)
    {
        try
        {
            Inventory? inventory =
                await _repository.GetByBranchAndDrinkAsync(branchId, drinkId);

            if (inventory == null)
            {
                inventory = new Inventory
                {
                    BranchId = branchId,
                    DrinkId = drinkId,
                    StockQuantity = quantity
                };

                await _repository.AddAsync(inventory);

                return Results.Created(
                    $"/api/inventory/{branchId}/{drinkId}",
                    ToResponse(inventory)
                );
            }

            inventory.StockQuantity = (inventory.StockQuantity ?? 0) + quantity;
            await _repository.UpdateAsync(inventory);

            return Results.Ok(ToResponse(inventory));
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.InnerException?.Message ?? ex.Message);
        }
    }

    public async Task<IResult> GetStockLevelsAsync(int branchId)
    {
        IEnumerable<Inventory> stock = await _repository.GetByBranchAsync(branchId);
        return stock == null ? Results.NotFound("No stock info found for this branch") : Results.Ok(stock);
    }

    #region Helpers
    private static InventoryResponseDto ToResponse(Inventory inventory)
    {
        return new InventoryResponseDto
        {
            InventoryId = inventory.InventoryId,
            BranchId = inventory.BranchId ?? 0,
            DrinkId = inventory.DrinkId ?? 0,
            StockQuantity = inventory.StockQuantity ?? 0
        };
    }
    #endregion
}
