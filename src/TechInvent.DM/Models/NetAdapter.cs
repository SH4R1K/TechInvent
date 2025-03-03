using System;
using System.Collections.Generic;

namespace TechInvent.DM.Models;

public partial class NetAdapter : Component
{
    public int IdComponent { get; set; }

    public string? MacAddress { get; set; }

    public int IdManufacturer { get; set; }

    public int IdAdapterType { get; set; }

    public virtual AdapterType AdapterType { get; set; } = null!;

    public virtual Component Component { get; set; } = null!;

    public virtual Manufacturer Manufacturer { get; set; } = null!;
}
