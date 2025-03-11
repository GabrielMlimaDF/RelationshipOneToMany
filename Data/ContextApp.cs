using Microsoft.EntityFrameworkCore;
using Relação1N.Data.Mapping;
using Relação1N.Models;

namespace Relação1N.Data
{
    public class ContextApp : DbContext
    {
        public ContextApp(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserMap());
        }
    }
}