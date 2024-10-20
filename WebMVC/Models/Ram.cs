using System;
using System.Collections.Generic;

namespace TechInventAPI.Models;

public partial class Ram : Component
{
    public int IdComponent { get; set; }

    public string? Speed { get; set; }

    public string? Capacity { get; set; }

    public string? MemoryType { get; set; }

    public string? PartNumber { get; set; }

    public string? SerialNumber { get; set; }

    public int IdManufacturer { get; set; }

    public virtual Component IdComponentNavigation { get; set; } = null!;

    public virtual Manufacturer IdManufacturerNavigation { get; set; } = null!;
}
