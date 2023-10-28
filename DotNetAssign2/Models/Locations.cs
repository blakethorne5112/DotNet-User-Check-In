using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace DotNetAssign2.Models
{
    /// <summary>
    /// The class for locations.
    /// </summary>
    public class Locations
    {
        // [Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        public string? Description { get; set;}
        [Required]
        [Url]
        public string? MapsLink { get; set; }

        public List<UsersLocations> UsersLocations { get; } = default!;
    }

/*    public class UsersDBContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
    }*/
}
