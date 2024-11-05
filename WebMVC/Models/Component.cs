using System;
using System.Collections.Generic;

namespace WebMVC.Models;

public partial class Component
{
    public int IdComponent { get; set; }

    public string? Name { get; set; }

    public int IdWorkplace { get; set; }

    public virtual Gpu? Gpu { get; set; }

    public virtual Workplace IdWorkplaceNavigation { get; set; } = null!;

    public virtual Mainboard? Mainboard { get; set; }

    public virtual NetAdapter? NetAdapter { get; set; }

    public virtual Processor? Processor { get; set; }

    public virtual Ram? Ram { get; set; }
    
    public virtual Disk? Disk { get; set; }
}
