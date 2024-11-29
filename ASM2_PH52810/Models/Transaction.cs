using System;
using System.Collections.Generic;

namespace ASM2_PH52810.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? PlayerId { get; set; }

    public int? ItemId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public virtual Item? Item { get; set; }

    public virtual Player? Player { get; set; }
}
