namespace TechInvent.DM.Models
{
    public class Software
    {
        public int IdSoftware { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public int IdManufacturer { get; set; }
        public virtual List<InstalledSoftware> InstalledSoftware { get; set; } = new List<InstalledSoftware>();
        public Manufacturer ManufacturerNavigation { get; set; }
    }
}
