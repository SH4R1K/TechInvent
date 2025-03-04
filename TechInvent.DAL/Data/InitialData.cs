using TechInvent.DM.Models;

namespace TechInvent.DAL.Data
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
        public static List<RequestType> RequestTypes { get; set; } = new List<RequestType>()
        {
            new RequestType {IdRequestType = 1, Name = "Проблема с ПК"},
            new RequestType {IdRequestType = 2, Name = "Проблема с периферией"},
            new RequestType {IdRequestType = 3, Name = "Проблема с ПО"},
            new RequestType {IdRequestType = 4, Name = "Проблема в кабинете"},
            new RequestType {IdRequestType = 5, Name = "Другое"},
        };
    }
}
