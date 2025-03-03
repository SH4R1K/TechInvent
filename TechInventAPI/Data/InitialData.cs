using TechInvent.DM.Models;

namespace TechInventAPI.Data
{
    public static class InitialData
    {
        public static List<User> UsersList { get; set; } = new List<User>()
        {
            new User {IdUser=1, Login="admin", Password="1", IdRole=1},
        };
        public static List<Role> RolesList { get; set; } = new List<Role>()
        {
           new Role {IdRole=1, Name="admin"},
           new Role {IdRole=2, Name="user"},
        };
    }
}
