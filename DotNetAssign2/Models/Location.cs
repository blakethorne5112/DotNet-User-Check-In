using System.ComponentModel.DataAnnotations;
using DotNetAssign2.Data;
using Microsoft.EntityFrameworkCore;

namespace DotNetAssign2.Models
{
    public class Location : ApplicationDbContext
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(35)]
        public string Name { get; set; } = "";

        [Required]
        [MinLength(3)]
        public string Description { get; set; } = "";

        public DateTime Date { get; set; }

        public ICollection<Models.UserLocation> UserLocations { get; } = new List<Models.UserLocation>();
    }
}
