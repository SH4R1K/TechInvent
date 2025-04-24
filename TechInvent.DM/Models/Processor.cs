namespace TechInvent.DM.Models;

public partial class Processor : Component
{
    public int IdComponent { get; set; }

    public string? PhysicalCores { get; set; }

    public string? LogicalCores { get; set; }

    public string? MaxClockSpeed { get; set; }

    public virtual Component IdComponentNavigation { get; set; } = null!;
    public override bool Equals(object? obj)
    {
        if (obj is Processor other)
        {
            return Name == other.Name &&
                   PhysicalCores == other.PhysicalCores &&
                   LogicalCores == other.LogicalCores &&
                   MaxClockSpeed == other.MaxClockSpeed;
        }
        return false;
    }
}
