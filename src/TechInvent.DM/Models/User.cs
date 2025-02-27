namespace TechInvent.DM.Models
{
    public class User
    {
        public int IdUser { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int IdRole { get; set; }
        public Role Role { get; set; }

    }
}
