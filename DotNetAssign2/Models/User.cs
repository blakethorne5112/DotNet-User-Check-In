using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DotNetAssign2.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        public ICollection<UserLocation> UserLocations { get; } = new List<UserLocation>(); 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.UserLocations)
                .WithOne(ue => ue.User)
                .HasForeignKey(ue => ue.UserId);
        }
    }

/*    public class UsersDBContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
    }*/
}
