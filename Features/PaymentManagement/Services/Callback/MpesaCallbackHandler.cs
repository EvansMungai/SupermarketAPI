using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Supermarket.API.Features.InventoryManagement.Services.Repositories;
using Supermarket.API.Features.PaymentManagement.Models.Callback;
using Supermarket.API.Features.PaymentManagement.Services.Payment.Repositories;
using Supermarket.API.Features.SalesManagement.Services.Repositories;

namespace Supermarket.API.Features.PaymentManagement.Services.Callback;

public class MpesaCallbackHandler : IMpesaCallbackHandler
{
    private readonly IMemoryCache _memoryCache;
    private readonly IMpesaApiService _mpesaApi;
    private readonly ISaleRepository _saleRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IPaymentRepository _paymentRepository;

    public MpesaCallbackHandler(IMemoryCache memoryCache, IMpesaApiService mpesaApi, ISaleRepository saleRepository, IInventoryRepository inventoryRepository, IPaymentRepository paymentRepository)
    {
        _memoryCache = memoryCache;
        _mpesaApi = mpesaApi;
        _saleRepository = saleRepository;
        _inventoryRepository = inventoryRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<IResult> HandleAsync(HttpRequest request)
    {
        using var reader = new StreamReader(request.Body);
        var rawBody = await reader.ReadToEndAsync();

        MpesaCallbackModel? callback = JsonConvert.DeserializeObject<MpesaCallbackModel>(rawBody);
        var stk = callback?.Body?.stkCallback;
        if (stk == null)
            return Results.BadRequest("Missing stkCallback");

        var metadata = stk.CallbackMetadata?.Item;
        if (metadata == null)
            return Results.BadRequest("Missing CallbackMetadata");

        string transactionId = _mpesaApi.GetValue(metadata, "MpesaReceiptNumber");
        decimal amount = Convert.ToDecimal(_mpesaApi.GetValue(metadata, "Amount"));

        // Lookup payment_id from cache
        if (!_memoryCache.TryGetValue($"payment-{stk.CheckoutRequestID}", out int paymentId))
            return Results.NotFound("No pending payment found.");

        var payment = await _paymentRepository.GetByIdAsync(paymentId);
        if (payment == null)
            return Results.NotFound("Payment record not found.");

        // Update payment record
        payment.TransactionId = transactionId;
        payment.Status = stk.ResultCode == 0 ? "Success" : "Failed";
        await _paymentRepository.UpdateAsync(payment);

        // If success, finalize sale (deduct inventory)
        if (stk.ResultCode == 0)
        {
            var sale = await _saleRepository.GetByIdAsync(payment.SaleId);
            var inventory = await _inventoryRepository.GetByBranchAndDrinkAsync(sale.BranchId, sale.DrinkId);
            if (inventory != null && (inventory.StockQuantity ?? 0) >= sale.Quantity)
            {
                inventory.StockQuantity -= sale.Quantity;
                await _inventoryRepository.UpdateAsync(inventory);
            }
        }
        // Remove cache entry
        _memoryCache.Remove($"payment-{stk.CheckoutRequestID}");
        return Results.Ok(new
        {
            Status = payment.Status,
            TransactionId = payment.TransactionId,
            SaleId = payment.SaleId
        });
    }
}
