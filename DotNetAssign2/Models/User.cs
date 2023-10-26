using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DotNetAssign2.Data;

namespace DotNetAssign2.Models
{
    public class User : IdentityUser
    {
        [Required]
        [EmailAddress]
        public override string? Email { get; set;}
        // [Required]
        // [StringLength(10)]
        // public string? Phone { get; set; }
        public bool CheckedIn { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }

        public bool IsAdmin { get; set; } = false;

        public List<UserEvent> UserEvents { get; } = new (); 
    }

/*    public class UsersDBContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
    }*/
}
