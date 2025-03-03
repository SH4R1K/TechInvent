using System;
using System.Collections.Generic;

namespace TechInvent.DM.Models;

public partial class AdapterType
{
    public int IdAdapterType { get; set; }

    public string? Name { get; set; }

    public virtual List<NetAdapter> NetAdapters { get; set; } = new List<NetAdapter>();
}
