using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Supermarket.API.Features.PaymentManagement.Models.Common;
using Supermarket.API.Features.PaymentManagement.Models.LipaNaMpesa;
using Supermarket.API.Features.PaymentManagement.Services.Payment.Repositories;
using Supermarket.API.Features.SalesManagement.Models;
using System.Text;

namespace Supermarket.API.Features.PaymentManagement.Services.Payment;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repository;
    private readonly IMpesaApiService _mpesaApiService;
    private readonly MpesaConfig _mpesaConfig;

    public PaymentService(IPaymentRepository repository, IMpesaApiService api, IOptions<MpesaConfig> config)
    {
        _repository = repository;
        _mpesaApiService = api;
        _mpesaConfig = config.Value;
    }

    public async Task<IResult> CreatePaymentAsync(Supermarket.API.Features.PaymentManagement.Models.Payment payment)
    {
        try
        {
            payment.CreatedAt = DateTime.UtcNow;
            if (string.IsNullOrEmpty(payment.Status))
            {
                payment.Status = "Pending";
            }

            await _repository.AddAsync(payment);
            return Results.Created($"/api/payments/{payment.PaymentId}", payment);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    public async Task<IResult> GetAllPaymentsAsync()
    {
        var payments = await _repository.GetAllAsync();
        return payments == null || !payments.Any() ? Results.NotFound("No payments found") : Results.Ok(payments);
    }

    public async Task<IResult> GetPaymentByIdAsync(int id)
    {
        var payment = await _repository.GetByIdAsync(id);
        return payment == null ? Results.NotFound($"Payment with ID = {id} not found") : Results.Ok(payment);
    }

    public async Task<LipaNaMpesaResponseModel> InitiateStkPushAsync(PendingSaleDto sale)
    {
        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        string password = Convert.ToBase64String(
            Encoding.UTF8.GetBytes(_mpesaConfig.BusinessShortCode + _mpesaConfig.Passkey + timestamp));
        var callbackUri = Environment.GetEnvironmentVariable("MpesaConfig__CallbackUri");

        var payload = new LipaNaMpesaRequestModel
        {
            BusinessShortCode = int.Parse(_mpesaConfig.BusinessShortCode),
            Password = password,
            Timestamp = timestamp,
            TransactionType = MpesaTransactionType.CustomerPayBillOnline,
            Amount = sale.TotalAmount,
            PartyA = sale.PhoneNumber,
            PartyB = _mpesaConfig.BusinessShortCode,
            PhoneNumber = sale.PhoneNumber,
            CallBackUrl = _mpesaConfig.CallbackUri,
            AccountReference = "Supermarket LTD",
            TransactionDescription = $"Payment for order {sale.SaleId}"
        };
        var payloadJson = JsonConvert.SerializeObject(payload);
        Console.WriteLine($"This is the callback url: {payload.CallBackUrl }");
        string baseUri = SystemEnvironmentUrl.Sandbox;
        string uri = baseUri + "mpesa/stkpush/v1/processrequest";

        Console.WriteLine(payloadJson);
        return await _mpesaApiService.LipaNaMpesa(uri, payload);
    }
}
