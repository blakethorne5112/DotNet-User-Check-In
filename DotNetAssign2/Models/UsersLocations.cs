using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DotNetAssign2.Models
{
    /// <summary>
    /// This class is used to store the check-in and check-out times of users at locations.
    /// </summary>
    [PrimaryKey(nameof(UsersID), nameof(LocationsId))]
    public class UsersLocations
    {
        public int UsersID { get; set; }

        public int LocationsId { get; set; }

        public bool CheckedIn { get; set; }
        [Required]
        public DateTime CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; } = null;
    }
}