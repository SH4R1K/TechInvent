namespace TechInventAPI.Models
{
    public class InstalledSoftware
    {
        public int IdWorkplace { get; set; }
        public int IdSoftware { get; set; }
        public Workplace WorkplaceNavigation { get; set; }
        public Software SoftwareNavigation { get; set; }
    }
}
