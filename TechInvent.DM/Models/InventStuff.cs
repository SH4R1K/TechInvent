namespace TechInvent.DM.Models
{
    public class InventStuff
    {
        public int IdInventStuff { get; set; }
        public required string Name { get; set; }
        public string? SerialNumber { get; set; }
        public string? InventNumber { get; set; }
        public bool IsDecommissioned { get; set; } = false;
    }
}
