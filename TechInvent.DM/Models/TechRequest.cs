namespace TechInvent.DM.Models
{
    public class TechRequest
    {
        public int IdRequest { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<TechRequestWorkplace> AttachedWorkplaces { get; set; } = new List<TechRequestWorkplace>();
    }
}
