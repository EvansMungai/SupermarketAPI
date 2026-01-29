using Supermarket.API.Features.Reporting.Models;

namespace Supermarket.API.Features.Reporting.Services.Repositories;

public interface IReportRepository
{
    Task<IEnumerable<SalesReport>> GetSalesByBranchAsync(int branchId);
    Task<IEnumerable<SalesReport>> GetSalesByDrinkTypeAsync(int drinkId);
    Task<decimal> GetTotalRevenueAsync();
}
