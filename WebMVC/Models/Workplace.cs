using System;
using System.Collections.Generic;

namespace WebMVC.Models;

public partial class Workplace
{
    public int IdWorkplace { get; set; }

    public int IdCabinet { get; set; }

    public int IdOs { get; set; }

    public string? Name { get; set; }
    public Guid Guid { get; set; } = Guid.NewGuid();
    public DateTime LastUpdate { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Component> Components { get; set; } = new List<Component>();

    public virtual ICollection<InstalledSoftware> InstalledSoftware { get; set; } = new List<InstalledSoftware>();

    public virtual Cabinet IdCabinetNavigation { get; set; } = null!;

    public virtual Os IdOsNavigation { get; set; } = null!;
}
