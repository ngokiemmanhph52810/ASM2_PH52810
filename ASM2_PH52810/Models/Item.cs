using System;
using System.Collections.Generic;

namespace ASM2_PH52810.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string? ItemName { get; set; }

    public int? ExperienceCost { get; set; }

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
