using System;
using System.Collections.Generic;
using Supermarket.API.Features.BranchManagement.Models;
using Supermarket.API.Features.DrinkManagement.Models;
using Supermarket.API.Features.PaymentManagement.Models;

namespace Supermarket.API.Features.SalesManagement.Models;

public partial class Sale
{
    public int SaleId { get; set; }

    public string? UserId { get; set; }

    public int BranchId { get; set; }

    public int DrinkId { get; set; }

    public int Quantity { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual Drink? Drink { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
