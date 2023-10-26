using Microsoft.AspNetCore.Identity;

namespace DotNetAssign2.Models
{
    public class ManagerUsers : IdentityUser
    {
        public PermissionLevel Role { get; set; }

        public enum PermissionLevel
        {
            Guest,
            User,
            Moderator,
            Admin,
        }

    }
}
