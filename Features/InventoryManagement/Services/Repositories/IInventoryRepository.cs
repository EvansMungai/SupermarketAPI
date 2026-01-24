using Supermarket.API.Features.InventoryManagement.Models;

namespace Supermarket.API.Features.InventoryManagement.Services.Repositories;

public interface IInventoryRepository
{
    Task<Inventory?> GetByBranchAndDrinkAsync(int branchId, int drinkId);
    Task<IEnumerable<Inventory>> GetByBranchAsync(int branchId);
    Task AddAsync(Inventory inventory);
    Task UpdateAsync(Inventory inventory);
}
