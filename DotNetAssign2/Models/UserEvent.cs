using System.ComponentModel.DataAnnotations;

namespace DotNetAssign2.Models
{
    public class UserEvent
    {
        [Required]
        public int UserID { get; set; }
        public Users User { get; set; } = null!;

        [Required]
        public int EventID { get; set; }
        public Event Event { get; set; } = null!;

        [Required]
        public DateTime CheckinTime { get; set; }

        public DateTime? CheckoutTime { get; set; } = null;
    }
}
