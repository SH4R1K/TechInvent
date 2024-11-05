using System;
using System.Collections.Generic;

namespace WebMVC.Models;

public partial class NetAdapter : Component
{
    public int IdComponent { get; set; }

    public string? MacAddress { get; set; }

    public int IdManufacturer { get; set; }

    public int AdapterTypeIdAdapterType { get; set; }

    public virtual AdapterType AdapterTypeIdAdapterTypeNavigation { get; set; } = null!;

    public virtual Component IdComponentNavigation { get; set; } = null!;

    public virtual Manufacturer IdManufacturerNavigation { get; set; } = null!;
}
