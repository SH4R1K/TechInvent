namespace TechInvent.DM.Models
{
    public class User
    {
        public int IdUser { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoginDate{ get; set; }
        public int IdRole { get; set; }
        public Role Role { get; set; }
        public List<TechRequestComment> Comments = new List<TechRequestComment>();
        public List<TechRequest> TechRequests = new List<TechRequest>();
    }
}
