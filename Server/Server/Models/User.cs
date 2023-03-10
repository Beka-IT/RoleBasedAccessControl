using Server.Enums;

namespace Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public RoleType Role { get; set; }
    }
}
