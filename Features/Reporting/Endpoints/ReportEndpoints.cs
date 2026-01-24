using Microsoft.AspNetCore.Mvc;
using Supermarket.API.Features.Reporting.Services;

namespace Supermarket.API.Features.Reporting.Endpoints;

public static class ReportEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/reports")
                       .WithTags("Reports");

        group.MapGet("/sales-by-branch", async (DateTime? start, DateTime? end, ReportService service) =>
        {
            var startDate = start ?? DateTime.MinValue;
            var endDate = end ?? DateTime.MaxValue;
            return await service.GetSalesByBranchAsync(startDate, endDate);
        });

        group.MapGet("/sales-by-drink", async (DateTime? start, DateTime? end, ReportService service) =>
        {
            var startDate = start ?? DateTime.MinValue;
            var endDate = end ?? DateTime.MaxValue;
            return await service.GetSalesByDrinkTypeAsync(startDate, endDate);
        });

        group.MapGet("/total-revenue", async (DateTime? start, DateTime? end, ReportService service) =>
        {
            var startDate = start ?? DateTime.MinValue;
            var endDate = end ?? DateTime.MaxValue;
            return await service.GetTotalRevenueAsync(startDate, endDate);
        });
    }
}
