using Microsoft.AspNetCore.Mvc;
using Supermarket.API.Features.SalesManagement.Models;
using Supermarket.API.Features.SalesManagement.Services;

namespace Supermarket.API.Features.SalesManagement.Endpoints;

public static class SaleEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/sales")
                       .WithTags("Sales");

        group.MapPost("/", async (SaleRequest request, SaleService service) =>
            await service.CreateSaleAsync(request));

        group.MapGet("/", async (SaleService service) =>
            await service.GetAllSalesAsync());

        group.MapGet("/{id}", async (int id, SaleService service) =>
            await service.GetSaleByIdAsync(id));
    }
}
