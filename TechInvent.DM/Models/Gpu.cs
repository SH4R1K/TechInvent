using System;
using System.Collections.Generic;

namespace TechInvent.DM.Models;

public partial class Gpu : Component
{
    public int IdComponent { get; set; }

    public string? AdapterRam { get; set; }

    public string? Uuid { get; set; }

    public virtual Component IdComponentNavigation { get; set; } = null!;
    public override bool Equals(object? obj)
    {
        if (obj is Gpu other)
        {
            return Name == other.Name &&
                   AdapterRam == other.AdapterRam &&
                   Uuid == other.Uuid;
        }
        return false;
    }
}
