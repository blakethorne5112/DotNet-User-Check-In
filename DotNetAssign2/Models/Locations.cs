using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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
        public string? Description { get; set;}
        [Required]
        [Url]
        public string? MapsLink { get; set; }
    }
}
