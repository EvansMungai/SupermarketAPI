using Supermarket.API.Features.BranchManagement.Models;
using Supermarket.API.Features.BranchManagement.Services.Repositories;

namespace Supermarket.API.Features.BranchManagement.Services;

public class BranchService
{
    private readonly IBranchRepository _repository;

    public BranchService(IBranchRepository repository)
    {
        _repository = repository;
    }

    public async Task<IResult> GetAllBranchesAsync()
    {
        IEnumerable<Branch> branches = await _repository.GetAllAsync();
        return branches == null || !branches.Any() ? Results.NotFound("No branches found") : Results.Ok(branches);
    }

    public async Task<IResult> GetBranchByIdAsync(int id)
    {
        Branch? branch = await _repository.GetByIdAsync(id);
        return branch == null ? Results.NotFound($"Branch with ID = {id} was not found") : Results.Ok(branch);
    }

    public async Task<IResult> CreateBranchAsync(Branch branch)
    {
        try
        {
            await _repository.AddAsync(branch);
            return Results.Created($"/api/branches/{branch.BranchId}", branch);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.InnerException?.Message ?? ex.Message);
        }
    }

    public async Task<IResult> UpdateBranchAsync(Branch branch)
    {
        try
        {
            Branch? existing = await _repository.GetByIdAsync(branch.BranchId);
            if (existing == null) return Results.NotFound($"Branch with ID = {branch.BranchId} was not found");

            await _repository.UpdateAsync(branch);
            Branch? updated = await _repository.GetByIdAsync(branch.BranchId);
            return Results.Ok(updated);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.InnerException?.Message ?? ex.Message);
        }
    }

    public async Task<IResult> DeleteBranchAsync(int id)
    {
        try
        {
            Branch? existing = await _repository.GetByIdAsync(id);
            if (existing == null) return Results.NotFound($"Branch with ID = {id} was not found");

            await _repository.DeleteAsync(id);
            return Results.Ok(existing);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.InnerException?.Message ?? ex.Message);
        }
    }
}
