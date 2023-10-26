using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNetAssign2.Data
{
    public class ApplicationDbContext : IdentityDbContext<Models.User>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\\mssqllocaldb;Database=aspnet-DotNetAssign2-e2b8a38c-16be-41d8-838c-604bbed9fd3;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public DbSet<DotNetAssign2.Models.Location> Locations { get; set; } = default!;

        public DbSet<DotNetAssign2.Models.UserLocation> UserLocation { get; set; } = default!;

        // public DbSet<DotNetAssign2.Models.Users> Users { get; set; } = default!;
    }
}