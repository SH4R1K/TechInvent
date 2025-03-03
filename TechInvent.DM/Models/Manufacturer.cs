using System;
using System.Collections.Generic;

namespace TechInvent.DM.Models;

public partial class Manufacturer
{
    public int IdManufacturer { get; set; }

    public string? Name { get; set; }

    public virtual List<NetAdapter> NetAdapters { get; set; } = new List<NetAdapter>();

    public virtual List<Ram> Rams { get; set; } = new List<Ram>();

    public virtual List<Software> Softwares { get; set; } = new List<Software>();
}
