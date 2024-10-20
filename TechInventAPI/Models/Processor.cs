using System;
using System.Collections.Generic;

namespace TechInventAPI.Models;

public partial class Processor : Component
{
    public int IdComponent { get; set; }

    public string? PhysicalCores { get; set; }

    public string? LogicalCores { get; set; }

    public string? MaxClockSpeed { get; set; }

    public virtual Component IdComponentNavigation { get; set; } = null!;
}
