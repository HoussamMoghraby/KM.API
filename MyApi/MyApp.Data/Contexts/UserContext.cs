using Microsoft.EntityFrameworkCore;
using MyApp.Data.Models;

namespace MyApp.Data.Contexts
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
