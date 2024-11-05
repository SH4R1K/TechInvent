using System;
using System.Collections.Generic;

namespace TechInventAPI.Models;

public partial class Manufacturer
{
    public int IdManufacturer { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<NetAdapter> NetAdapters { get; set; } = new List<NetAdapter>();

    public virtual ICollection<Ram> Rams { get; set; } = new List<Ram>();

    public virtual ICollection<Software> Softwares { get; set; } = new List<Software>();
}
