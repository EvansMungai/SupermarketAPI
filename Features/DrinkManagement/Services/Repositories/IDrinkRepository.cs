using Supermarket.API.Features.DrinkManagement.Models;

namespace Supermarket.API.Features.DrinkManagement.Services.Repositories;

public interface IDrinkRepository
{
    Task<IEnumerable<Drink>> GetAllAsync();
    Task<Drink?> GetByIdAsync(int id);
    Task AddAsync(Drink drink);
    Task UpdateAsync(Drink drink);
    Task DeleteAsync(int id);
}
