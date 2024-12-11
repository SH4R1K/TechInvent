namespace TechInventAPI.Models
{
    public class Role
    {
        public int IdRole { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
