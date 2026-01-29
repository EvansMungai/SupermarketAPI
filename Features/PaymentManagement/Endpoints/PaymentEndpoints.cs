using Supermarket.API.Features.PaymentManagement.Services.Callback;
using Supermarket.API.Features.PaymentManagement.Services.Payment;

namespace Supermarket.API.Features.PaymentManagement.Endpoints;

public static class PaymentEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/payments")
                       .WithTags("Payments");

        //group.MapPost("/", async (Supermarket.API.Features.PaymentManagement.Models.Payment payment, IPaymentService service) =>
        //    await service.CreatePaymentAsync(payment));

        group.MapGet("/", async (IPaymentService service) =>
            await service.GetAllPaymentsAsync());

        group.MapGet("/{id}", async (int id, IPaymentService service) =>
            await service.GetPaymentByIdAsync(id));

        group.MapPost("/callback", async (IMpesaCallbackHandler service, HttpRequest request) =>
            await service.HandleAsync(request));
    }
}
