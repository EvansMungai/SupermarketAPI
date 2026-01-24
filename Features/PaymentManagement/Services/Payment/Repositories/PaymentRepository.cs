using Microsoft.EntityFrameworkCore;
using Supermarket.API.Data.Infrastructure;
using Supermarket.API.Features.PaymentManagement.Models;

namespace Supermarket.API.Features.PaymentManagement.Services.Payment.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly SupermarketContext _context;

    public PaymentRepository(SupermarketContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Models.Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Models.Payment>> GetAllAsync()
    {
        return await _context.Payments.ToListAsync();
    }

    public async Task<Models.Payment?> GetByIdAsync(int id)
    {
        return await _context.Payments.FindAsync(id);
    }

    public async Task UpdateAsync(Models.Payment payment)
    {
        _context.Payments.Update(payment);
        await _context.SaveChangesAsync();
    }
}
