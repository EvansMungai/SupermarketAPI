namespace Supermarket.API.Features.PaymentManagement.Services.Payment.Repositories;

public interface IPaymentRepository
{
    Task AddAsync(Models.Payment payment);
    Task<Models.Payment?> GetByIdAsync(int id);
    Task<IEnumerable<Models.Payment>> GetAllAsync();
    Task UpdateAsync(Models.Payment payment);
}
