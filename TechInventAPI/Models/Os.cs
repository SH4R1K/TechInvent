using System;
using System.Collections.Generic;

namespace TechInventAPI.Models;

public partial class Os
{
    public int IdOs { get; set; }

    public string? OsName { get; set; }

    public string? OsVersion { get; set; }

    public virtual ICollection<Workplace> Workplaces { get; set; } = new List<Workplace>();
}
