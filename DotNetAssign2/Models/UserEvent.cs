using System.ComponentModel.DataAnnotations;
using DotNetAssign2.Data;
using Microsoft.EntityFrameworkCore;

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
    [PrimaryKey(nameof(UsersId), nameof(EventsId))]
    public class UserEvent : DbContext
    {
        public int UsersId { get; set; }
        public int EventsId { get; set; }

        public User User { get; set; } = null!;
        public Event Event { get; set; } = null!;

        [Required]
        public DateTime CheckinTime { get; set; }

        public DateTime? CheckoutTime { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEvent>()
                .Property(e => e.CheckinTime)
                .HasDefaultValueSql("getdate()");
        }
    }
}
