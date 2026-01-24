using Supermarket.API.Features.BranchManagement.Models;

namespace Supermarket.API.Features.BranchManagement.Services.Repositories;

public interface IBranchRepository
{
    Task<IEnumerable<Branch>> GetAllAsync();
    Task<Branch?> GetByIdAsync(int id);
    Task AddAsync(Branch branch);
    Task UpdateAsync(Branch branch);
    Task DeleteAsync(int id);
}
