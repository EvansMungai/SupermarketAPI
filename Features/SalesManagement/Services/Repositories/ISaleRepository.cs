using Supermarket.API.Features.SalesManagement.Models;

namespace Supermarket.API.Features.SalesManagement.Services.Repositories;

public interface ISaleRepository
{
    Task AddAsync(Sale sale);
    Task<IEnumerable<Sale>> GetAllAsync();
    Task<Sale?> GetByIdAsync(int id);
}
