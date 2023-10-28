using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace DotNetAssign2.Models
{
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
    }

/*    public class UsersDBContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
    }*/
}
