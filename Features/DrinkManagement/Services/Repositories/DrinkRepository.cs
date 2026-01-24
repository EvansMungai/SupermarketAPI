using Microsoft.EntityFrameworkCore;
using Supermarket.API.Data.Infrastructure;
using Supermarket.API.Features.DrinkManagement.Models;

namespace Supermarket.API.Features.DrinkManagement.Services.Repositories;

public class DrinkRepository : IDrinkRepository
{
    private readonly SupermarketContext _context;

    public DrinkRepository(SupermarketContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Drink>> GetAllAsync()
    {
        return await _context.Drinks.ToListAsync();
    }

    public async Task<Drink?> GetByIdAsync(int id)
    {
        return await _context.Drinks.Where(d => d.DrinkId == id).AsNoTracking().SingleOrDefaultAsync();
    }

    public async Task AddAsync(Drink drink)
    {
        await _context.Drinks.AddAsync(drink);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Drink drink)
    {
        _context.Drinks.Update(drink);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var drink = await _context.Drinks.FindAsync(id);
        if (drink != null)
        {
            _context.Drinks.Remove(drink);
            await _context.SaveChangesAsync();
        }
    }
}
