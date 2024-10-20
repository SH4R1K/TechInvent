using System;
using System.Collections.Generic;

namespace TechInventAPI.Models;

public partial class Workplace
{
    public int IdWorkplace { get; set; }

    public int IdCabinet { get; set; }

    public int IdOs { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Component> Components { get; set; } = new List<Component>();

    public virtual Cabinet IdCabinetNavigation { get; set; } = null!;

    public virtual Os IdOsNavigation { get; set; } = null!;
}
