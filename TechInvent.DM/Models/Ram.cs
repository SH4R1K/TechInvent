using System;
using System.Collections.Generic;

namespace TechInvent.DM.Models;

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

    public override bool Equals(object? obj)
    {
        if (obj is Ram other)
        {
            return Name == other.Name &&
                   Speed == other.Speed &&
                   Capacity == other.Capacity &&
                   MemoryType == other.MemoryType &&
                   PartNumber == other.PartNumber &&
                   SerialNumber == other.SerialNumber &&
                   IdManufacturer == other.IdManufacturer;
        }
        return false;
    }
}
