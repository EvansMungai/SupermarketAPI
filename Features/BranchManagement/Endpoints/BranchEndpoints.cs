using Microsoft.AspNetCore.Mvc;
using Supermarket.API.Features.BranchManagement.Models;
using Supermarket.API.Features.BranchManagement.Services;

namespace Supermarket.API.Features.BranchManagement.Endpoints;

public static class BranchEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/branches")
                       .WithTags("Branches");

        group.MapGet("/", async (BranchService service) =>
            await service.GetAllBranchesAsync());

        group.MapGet("/{id}", async (int id, BranchService service) =>
            await service.GetBranchByIdAsync(id));

        group.MapPost("/", async (Branch branch, BranchService service) =>
            await service.CreateBranchAsync(branch));

        group.MapPut("/{id}", async (int id, Branch branch, BranchService service) =>
        {
            if (id != branch.BranchId) return Results.BadRequest("Branch ID mismatch");
            return await service.UpdateBranchAsync(branch);
        });

        group.MapDelete("/{id}", async (int id, BranchService service) =>
            await service.DeleteBranchAsync(id));
    }
}
