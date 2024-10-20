using System;
using System.Collections.Generic;

namespace TechInventAPI.Models;

public partial class AdapterType
{
    public int IdAdapterType { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<NetAdapter> NetAdapters { get; set; } = new List<NetAdapter>();
}
