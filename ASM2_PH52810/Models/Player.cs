using System;
using System.Collections.Generic;

namespace ASM2_PH52810.Models;

public partial class Player
{
    public int PlayerId { get; set; }

    public string? PlayerName { get; set; }

    public string? Password { get; set; }

    public string? Mode { get; set; }

    public int? TotalExperience { get; set; }

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
