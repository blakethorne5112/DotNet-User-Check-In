using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DotNetAssign2.Models
{
    [PrimaryKey(nameof(UsersID), nameof(LocationsID))]
    public class UsersLocations
    {
        public int UsersID { get; set; }
        public Users Users { get; set; } = default!;

        public int LocationsID { get; set; }
        public Locations Locations { get; set; } = default!;

        public bool CheckedIn { get; set; }
        [Required]
        public DateTime CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; } = null;
    }

/*    public class UsersDBContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
    }*/
}