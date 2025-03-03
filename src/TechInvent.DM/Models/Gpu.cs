using System;
using System.Collections.Generic;

namespace TechInvent.DM.Models;

public partial class Gpu : Component
{
    public int IdComponent { get; set; }

    public string? AdapterRam { get; set; }

    public string? Uuid { get; set; }

    public virtual Component Component { get; set; } = null!;
}
