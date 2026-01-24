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

    public async Task<IResult> GetSalesByBranchAsync(DateTime start, DateTime end)
    {
        var report = await _repository.GetSalesByBranchAsync(start, end);
        return report == null ? Results.NotFound("No data found for the given criteria") : Results.Ok(report);
    }

    public async Task<IResult> GetSalesByDrinkTypeAsync(DateTime start, DateTime end)
    {
        var report = await _repository.GetSalesByDrinkTypeAsync(start, end);
        return report == null ? Results.NotFound("No data found for the given criteria") : Results.Ok(report);
    }

    public async Task<IResult> GetTotalRevenueAsync(DateTime start, DateTime end)
    {
        decimal revenue = await _repository.GetTotalRevenueAsync(start, end);
        return Results.Ok(new { TotalRevenue = revenue });
    }
}
