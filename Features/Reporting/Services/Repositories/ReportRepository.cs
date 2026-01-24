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

    public async Task<IEnumerable<SalesReport>> GetSalesByBranchAsync(DateTime start, DateTime end)
    {
        return await _context.Sales
            .Where(s => s.CreatedAt >= start && s.CreatedAt <= end)
            .GroupBy(s => s.Branch!.BranchName)
            .Select(g => new SalesReport
            {
                BranchName = g.Key,
                TotalRevenue = g.Sum(s => s.TotalAmount),
                ReportDate = DateTime.UtcNow
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<SalesReport>> GetSalesByDrinkTypeAsync(DateTime start, DateTime end)
    {
        return await _context.Sales
            .Where(s => s.CreatedAt >= start && s.CreatedAt <= end)
            .GroupBy(s => s.Drink!.DrinkName) // DrinkName instead of Category
            .Select(g => new SalesReport
            {
                DrinkCategory = g.Key,
                TotalRevenue = g.Sum(s => s.TotalAmount),
                ReportDate = DateTime.UtcNow
            })
            .ToListAsync();
    }

    public async Task<decimal> GetTotalRevenueAsync(DateTime start, DateTime end)
    {
        return await _context.Sales
            .Where(s => s.CreatedAt >= start && s.CreatedAt <= end)
            .SumAsync(s => s.TotalAmount);
    }
}
