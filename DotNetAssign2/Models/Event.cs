using System.ComponentModel.DataAnnotations;

namespace DotNetAssign2.Models
{
    public class Event
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

        [Required]
        public DateTime Date { get; set; }

        public List<UserEvent> UserEvents { get; } = new ();
    }
}
