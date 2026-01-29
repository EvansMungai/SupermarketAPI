using Microsoft.EntityFrameworkCore;
using Supermarket.API.Data.Infrastructure;
using Supermarket.API.Features.Reporting.Models;

namespace Supermarket.API.Features.Reporting.Services.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly SupermarketContext _context;

    public ReportRepository(SupermarketContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SalesReport>> GetSalesByBranchAsync(int branchId)
    {
        return await _context.Sales
            .Where(s => s.BranchId == branchId)
            .GroupBy(s => s.Branch!.BranchName)
            .Select(g => new SalesReport
            {
                BranchId = branchId,
                TotalRevenue = g.Sum(s => s.TotalAmount),
                ReportDate = DateTime.UtcNow
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<SalesReport>> GetSalesByDrinkTypeAsync(int drinkId)
    {
        return await _context.Sales
            .GroupBy(s => s.DrinkId == drinkId) 
            .Select(g => new SalesReport
            {
                DrinkId = drinkId,
                TotalRevenue = g.Sum(s => s.TotalAmount),
                ReportDate = DateTime.UtcNow
            })
            .ToListAsync();
    }

    public async Task<decimal> GetTotalRevenueAsync()
    {
        return await _context.Sales
            .SumAsync(s => s.TotalAmount);
    }
}
