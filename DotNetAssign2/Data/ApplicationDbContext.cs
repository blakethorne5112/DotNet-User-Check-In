using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNetAssign2.Data
{
    public class ApplicationDbContext : IdentityDbContext<Models.User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<DotNetAssign2.Models.Event> Events { get; set; } = default!;

        public DbSet<DotNetAssign2.Models.UserEvent> UserEvents { get; set; } = default!;

        // public DbSet<DotNetAssign2.Models.Users> Users { get; set; } = default!;
    }
}