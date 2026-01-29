using Microsoft.EntityFrameworkCore;
using Supermarket.API.Data.Infrastructure;
using Supermarket.API.Features.InventoryManagement.Models;
using Supermarket.API.Features.DrinkManagement.Models;

namespace Supermarket.API.Features.InventoryManagement.Services.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly SupermarketContext _context;

    public InventoryRepository(SupermarketContext context)
    {
        _context = context;
    }

    public async Task<Inventory?> GetByBranchAndDrinkAsync(int branchId, int drinkId)
    {
        return await _context.Inventories
            .Include(i => i.Drink)
            .FirstOrDefaultAsync(i => i.BranchId == branchId && i.DrinkId == drinkId);
    }

    public async Task<IEnumerable<Inventory>> GetByBranchAsync(int branchId)
    {
        return await _context.Inventories
            .Where(i => i.BranchId == branchId)
            .ToListAsync();
    }

    public async Task AddAsync(Inventory inventory)
    {
        await _context.Inventories.AddAsync(inventory);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Inventory inventory)
    {
        _context.Inventories.Update(inventory);
        await _context.SaveChangesAsync();
    }
}
