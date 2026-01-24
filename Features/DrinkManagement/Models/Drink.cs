using System;
using System.Collections.Generic;
using Supermarket.API.Features.InventoryManagement.Models;
using Supermarket.API.Features.SalesManagement.Models;

namespace Supermarket.API.Features.DrinkManagement.Models;

public partial class Drink
{
    public int DrinkId { get; set; }

    public string? DrinkName { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
