using Supermarket.API.Features.PaymentManagement.Models.LipaNaMpesa;
using Supermarket.API.Features.SalesManagement.Models;

namespace Supermarket.API.Features.PaymentManagement.Services.Payment;

public interface IPaymentService
{
    Task<IResult> CreatePaymentAsync(Supermarket.API.Features.PaymentManagement.Models.Payment payment);
    Task<IResult> GetAllPaymentsAsync();
    Task<IResult> GetPaymentByIdAsync(int id);
    Task<LipaNaMpesaResponseModel> InitiateStkPushAsync(PendingSaleDto sale);
}
