using Microsoft.Extensions.Caching.Memory;
using Supermarket.API.Features.DrinkManagement.Models;
using Supermarket.API.Features.DrinkManagement.Services.Repositories;
using Supermarket.API.Features.InventoryManagement.Models;
using Supermarket.API.Features.InventoryManagement.Services.Repositories;
using Supermarket.API.Features.PaymentManagement.Models;
using Supermarket.API.Features.PaymentManagement.Services.Payment;
using Supermarket.API.Features.PaymentManagement.Services.Payment.Repositories;
using Supermarket.API.Features.SalesManagement.Models;
using Supermarket.API.Features.SalesManagement.Services.Repositories;

namespace Supermarket.API.Features.SalesManagement.Services;

public class SaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IDrinkRepository _drinkRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentService _paymentService;
    private readonly IMemoryCache _memoryCache;

    public SaleService(
        ISaleRepository saleRepository,
        IInventoryRepository inventoryRepository,
        IDrinkRepository drinkRepository,
        IPaymentRepository paymentRepository,
        IPaymentService paymentService,
        IMemoryCache memoryCache
        )
    {
        _saleRepository = saleRepository;
        _inventoryRepository = inventoryRepository;
        _drinkRepository = drinkRepository;
        _paymentRepository = paymentRepository;
        _paymentService = paymentService;
        _memoryCache = memoryCache;
    }

    public async Task<IResult> CreateSaleAsync(SaleRequest request)
    {
        try
        {
            // 1. Validate drink
            Drink? drink = await _drinkRepository.GetByIdAsync(request.DrinkId);
            if (drink == null)
                return Results.NotFound($"Drink with ID = {request.DrinkId} not found.");

            // 2. Validate inventory
            Inventory? inventory = await _inventoryRepository.GetByBranchAndDrinkAsync(request.BranchId, request.DrinkId);
            if (inventory == null || (inventory.StockQuantity ?? 0) < request.Quantity)
                return Results.BadRequest("Insufficient stock.");

            // 3. Calculate total
            decimal total = (drink.Price ?? 0) * request.Quantity;

            // 4. Create sale record (not yet finalized)
            Sale sale = new Sale
            {
                UserId = request.UserId,
                BranchId = request.BranchId,
                DrinkId = request.DrinkId,
                Quantity = request.Quantity,
                TotalAmount = total,
                CreatedAt = DateTime.UtcNow
            };
            await _saleRepository.AddAsync(sale);

            // 5. Create payment record (Pending) 
            Payment payment = new Payment
            { 
                SaleId = sale.SaleId,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            }; 

            await _paymentRepository.AddAsync(payment); 
            
            // 6. Initiate STK Push 
            var stk = await _paymentService.InitiateStkPushAsync(new PendingSaleDto { SaleId = sale.SaleId, BranchId = request.BranchId, DrinkId = request.DrinkId, Quantity = request.Quantity, TotalAmount = total, PhoneNumber = request.PhoneNumber }); 
            if (stk == null || string.IsNullOrEmpty(stk.CheckoutRequestID)) return Results.BadRequest("STK Push failed"); 
            
            // 7. Store mapping CheckoutRequestID ? payment_id (in memory or DB) 
            _memoryCache.Set($"payment-{stk.CheckoutRequestID}", payment.PaymentId, TimeSpan.FromMinutes(15)); 
            return Results.Ok(new { SaleId = sale.SaleId, PaymentId = payment.PaymentId, CheckoutRequestID = stk.CheckoutRequestID, Amount = total, PhoneNumber = request.PhoneNumber }); 
            
            return Results.Created($"/api/sales/{sale.SaleId}", sale);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    public async Task<IResult> GetAllSalesAsync()
    {
        IEnumerable<Sale> sales = await _saleRepository.GetAllAsync();
        return sales == null ? Results.NotFound("No sales found") : Results.Ok(sales);
    }

    public async Task<IResult> GetSaleByIdAsync(int id)
    {
        Sale? sale = await _saleRepository.GetByIdAsync(id);
        return sale == null ? Results.NotFound($"Sale with ID = {id} was not found") : Results.Ok(sale);
    }
}
