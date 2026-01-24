using Microsoft.EntityFrameworkCore;
using Supermarket.API.Data.Infrastructure;
using Supermarket.API.Features.SalesManagement.Models;
using Supermarket.API.Features.BranchManagement.Models;
using Supermarket.API.Features.DrinkManagement.Models;

namespace Supermarket.API.Features.SalesManagement.Services.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly SupermarketContext _context;

    public SaleRepository(SupermarketContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Sale sale)
    {
        await _context.Sales.AddAsync(sale);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await _context.Sales
            .Include(s => s.Branch)
            .Include(s => s.Drink)
            .ToListAsync();
    }

    public async Task<Sale?> GetByIdAsync(int id)
    {
        return await _context.Sales
            .Include(s => s.Branch)
            .Include(s => s.Drink)
            .FirstOrDefaultAsync(s => s.SaleId == id);
    }
}
