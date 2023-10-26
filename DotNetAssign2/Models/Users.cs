using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DotNetAssign2.Models
{
    public class Users : IdentityUser
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        public override string? Email { get; set;}
        [Required]
        [StringLength(10)]
        public string? Phone { get; set; }
        public bool CheckedIn { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
    }

/*    public class UsersDBContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
    }*/
}
