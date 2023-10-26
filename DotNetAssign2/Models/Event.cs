using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DotNetAssign2.Models
{
    public class Event : DbContext
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

        public string? Location { get; set; }

        public DateTime Date { get; set; }

        public List<UserEvent> UserEvents { get; } = new ();
    }
}
