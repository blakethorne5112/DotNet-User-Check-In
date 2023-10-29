using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetAssign2.Models;

namespace DotNetAssign2.Data
{
    public class UsersContext : DbContext
    {
        public UsersContext (DbContextOptions<UsersContext> options)
            : base(options)
        {
        }

        public DbSet<DotNetAssign2.Models.Users> Users { get; set; } = default!;

        public DbSet<DotNetAssign2.Models.Locations> Locations { get; set; } = default!;
    }
}
