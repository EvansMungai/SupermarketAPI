using Microsoft.AspNetCore.Mvc;
using Supermarket.API.Features.InventoryManagement.Models;
using Supermarket.API.Features.InventoryManagement.Services;

namespace Supermarket.API.Features.InventoryManagement.Endpoints;

public static class InventoryEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/inventory")
                       .WithTags("Inventory");

        group.MapPost("/restock", async (RestockRequest request, InventoryService service) =>
            await service.RestockAsync(request.BranchId, request.DrinkId, request.Quantity));

        group.MapGet("/{branchId}", async (int branchId, InventoryService service) =>
            await service.GetStockLevelsAsync(branchId));
    }
}
