using Supermarket.API.Features.Reporting.Models;
using Supermarket.API.Features.Reporting.Services.Repositories;

namespace Supermarket.API.Features.Reporting.Services;

public class ReportService
{
    private readonly IReportRepository _repository;

    public ReportService(IReportRepository repository)
    {
        _repository = repository;
    }

    public async Task<IResult> GetSalesByBranchAsync(int branchId)
    {
        var report = await _repository.GetSalesByBranchAsync(branchId);
        return report == null ? Results.NotFound("No data found for the given criteria") : Results.Ok(report);
    }

    public async Task<IResult> GetSalesByDrinkTypeAsync(int drinkdId)
    {
        var report = await _repository.GetSalesByDrinkTypeAsync(drinkdId);
        return report == null ? Results.NotFound("No data found for the given criteria") : Results.Ok(report);
    }

    public async Task<IResult> GetTotalRevenueAsync()
    {
        decimal revenue = await _repository.GetTotalRevenueAsync();
        return Results.Ok(new { TotalRevenue = revenue });
    }
}
