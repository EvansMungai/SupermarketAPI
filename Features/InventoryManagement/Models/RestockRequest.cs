namespace Supermarket.API.Features.InventoryManagement.Models;

public record RestockRequest(int BranchId, int DrinkId, int Quantity);
