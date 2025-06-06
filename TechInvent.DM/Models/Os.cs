using System;
using System.Collections.Generic;

namespace TechInvent.DM.Models;

public partial class Os
{
    public int IdOs { get; set; }

    public string? OsName { get; set; }

    public string? OsVersion { get; set; }

    public virtual List<Workplace> Workplaces { get; set; } = new List<Workplace>();
}
