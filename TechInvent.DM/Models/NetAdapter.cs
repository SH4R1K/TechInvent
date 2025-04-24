using System;
using System.Collections.Generic;

namespace TechInvent.DM.Models;

public partial class NetAdapter : Component
{
    public int IdComponent { get; set; }

    public string? MacAddress { get; set; }

    public int IdManufacturer { get; set; }

    public int AdapterTypeIdAdapterType { get; set; }

    public virtual AdapterType AdapterTypeIdAdapterTypeNavigation { get; set; } = null!;

    public virtual Component IdComponentNavigation { get; set; } = null!;

    public virtual Manufacturer IdManufacturerNavigation { get; set; } = null!; 
    public override bool Equals(object? obj)
    {
        if (obj is NetAdapter other)
        {
            return Name == other.Name &&
                   MacAddress == other.MacAddress &&
                   IdManufacturer == other.IdManufacturer &&
                   AdapterTypeIdAdapterType == other.AdapterTypeIdAdapterType;
        }
        return false;
    }
}
