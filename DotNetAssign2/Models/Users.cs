using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace DotNetAssign2.Models
{
    /// <summary>
    /// The anonymous user class.
    /// </summary>
    public class Users
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set;}
        [Required]
        [StringLength(10)]
        public string? Phone { get; set; }
        public bool CheckedIn { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }

        public List<UsersLocations> UsersLocations { get; } = default!;
    }

/*    public class UsersDBContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
    }*/
}
