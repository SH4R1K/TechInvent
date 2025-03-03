namespace TechInvent.DM.Models
{
    public class InstalledSoftware
    {
        public int IdWorkplace { get; set; }
        public int IdSoftware { get; set; }
        public Workplace Workplace { get; set; }
        public Software Software { get; set; }
    }
}
