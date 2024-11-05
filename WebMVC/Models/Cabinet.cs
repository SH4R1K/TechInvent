using System;
using System.Collections.Generic;

namespace WebMVC.Models;

public partial class Cabinet
{
    public int IdCabinet { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Workplace> Workplaces { get; set; } = new List<Workplace>();
}
