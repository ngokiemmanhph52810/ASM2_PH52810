using System;
using System.Collections.Generic;

namespace ASM2_PH52810.Models;

public partial class Purchase
{
    public int PurchaseId { get; set; }

    public int? PlayerId { get; set; }

    public int? ItemId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public virtual Item? Item { get; set; }

    public virtual Player? Player { get; set; }
}
