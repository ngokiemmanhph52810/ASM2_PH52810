using System;
using System.Collections.Generic;

namespace ASM2_PH52810.Models;

public partial class Resource
{
    public int ResourceId { get; set; }

    public string? ResourceName { get; set; }

    public string? ResourceType { get; set; }

    public int? Value { get; set; }
}
