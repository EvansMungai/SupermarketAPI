using Microsoft.EntityFrameworkCore;
using Supermarket.API.Data.Infrastructure;
using Supermarket.API.Features.BranchManagement.Models;
// using Supermarket.API.Features.BranchManagement.Models; // Branch is now in Data.Infrastructure

namespace Supermarket.API.Features.BranchManagement.Services.Repositories;

public class BranchRepository : IBranchRepository
{
    private readonly SupermarketContext _context;

    public BranchRepository(SupermarketContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Branch>> GetAllAsync()
    {
        return await _context.Branches.AsNoTracking().ToListAsync();
    }

    public async Task<Branch?> GetByIdAsync(int id)
    {
        return await _context.Branches.AsNoTracking().Where(b => b.BranchId == id).SingleOrDefaultAsync();
    }

    public async Task AddAsync(Branch branch)
    {
        await _context.Branches.AddAsync(branch);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Branch branch)
    {
        _context.Branches.Update(branch);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var branch = await _context.Branches.FindAsync(id);
        if (branch != null)
        {
            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();
        }
    }
}
