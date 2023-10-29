using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DotNetAssign2.Models
{
    /// <summary>
    /// The anonymous user class.
    /// </summary>
    public class Users
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set;}
        [Required]
        [StringLength(10)]
        public string? Phone { get; set; }
    }
}
