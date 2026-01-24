using Supermarket.API.Features.Reporting.Models;

namespace Supermarket.API.Features.Reporting.Services.Repositories;

public interface IReportRepository
{
    Task<IEnumerable<SalesReport>> GetSalesByBranchAsync(DateTime start, DateTime end);
    Task<IEnumerable<SalesReport>> GetSalesByDrinkTypeAsync(DateTime start, DateTime end);
    Task<decimal> GetTotalRevenueAsync(DateTime start, DateTime end);
}
