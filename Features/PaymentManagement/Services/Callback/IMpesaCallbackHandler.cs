namespace Supermarket.API.Features.PaymentManagement.Services.Callback;

public interface IMpesaCallbackHandler
{
    Task<IResult> HandleAsync(HttpRequest request);
}
