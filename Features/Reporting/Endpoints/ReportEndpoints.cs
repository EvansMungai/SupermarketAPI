using Supermarket.API.Features.Reporting.Services;

namespace Supermarket.API.Features.Reporting.Endpoints;

public static class ReportEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/reports")
                       .WithTags("Reports");

        group.MapGet("/sales-by-branch", async (int branchId, ReportService service) =>
        {
            return await service.GetSalesByBranchAsync(branchId);
        });

        group.MapGet("/sales-by-drink", async (int drinkId, ReportService service) =>
        {

            return await service.GetSalesByDrinkTypeAsync(drinkId);
        });

        group.MapGet("/total-revenue", async (ReportService service) =>
        {

            return await service.GetTotalRevenueAsync();
        });
    }
}
