using System;
using System.Collections.Generic;
using Supermarket.API.Features.BranchManagement.Models;
using Supermarket.API.Features.DrinkManagement.Models;

namespace Supermarket.API.Features.InventoryManagement.Models;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int? BranchId { get; set; }

    public int? DrinkId { get; set; }

    public int? StockQuantity { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual Drink? Drink { get; set; }
}
