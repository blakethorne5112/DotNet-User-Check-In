using System.ComponentModel.DataAnnotations;
using DotNetAssign2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

/// <summary>
/// This class is used to represent the relationship between a user and an event.
/// The primary key is the combination of the User and Event foreign keys.
/// </summary>
/// <remarks>
/// This class is used to represent the relationship between a user and an event.
/// The primary key is the combination of the User and Event foreign keys.
/// </remarks>

namespace DotNetAssign2.Models
{
    [PrimaryKey(nameof(UserId), nameof(LocationId))]
    public class UserLocation : ApplicationDbContext
    {
        public string UserId { get; set; } = null!;
        public int LocationId { get; set; }

        public User User { get; set; } = null!;
        public Location Location { get; set; } = null!;

        public DateTime CheckinTime { get; set; }
        public DateTime? CheckoutTime { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLocation>()
                .HasOne(e => e.User)
                .WithMany(e => e.UserLocations)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            modelBuilder.Entity<UserLocation>()
                .HasOne(e => e.Location)
                .WithMany(e => e.UserLocations)
                .HasForeignKey(e => e.LocationId)
                .IsRequired();
        }
    }
}
