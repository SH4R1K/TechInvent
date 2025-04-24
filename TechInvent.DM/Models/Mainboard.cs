using System;
using System.Collections.Generic;

namespace TechInvent.DM.Models;

public partial class Mainboard : Component
{
    public int IdComponent { get; set; }

    public string? SerialNumber { get; set; }

    public virtual Component IdComponentNavigation { get; set; } = null!;
    public override bool Equals(object? obj)
    {
        if (obj is Mainboard other)
        {
            return Name == other.Name &&
                   SerialNumber == other.SerialNumber;
        }
        return false;
    }
}
