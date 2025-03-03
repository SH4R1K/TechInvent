namespace TechInvent.DM.Models
{
    public class Role
    {
        public int IdRole { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
