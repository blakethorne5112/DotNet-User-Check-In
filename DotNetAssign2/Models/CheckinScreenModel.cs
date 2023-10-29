using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotNetAssign2.Models
{
    public class CheckinScreenModel : Users
    {
        [Required]
        public IEnumerable<SelectListItem> Locations { get; set; } = default!;
    }
}