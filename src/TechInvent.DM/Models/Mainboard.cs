using System;
using System.Collections.Generic;

namespace TechInvent.DM.Models;

public partial class Mainboard : Component
{
    public int IdComponent { get; set; }

    public string? SerialNumber { get; set; }

    public virtual Component Component { get; set; } = null!;
}
