using Microsoft.EntityFrameworkCore;
using OrganizationAPI.Models;

namespace OrganizationAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
        public DbSet<Organizations> Organizations { get; set; }
        public DbSet<OrganizationRole> OrganizationRole { get; set; }
        public DbSet<RoleUser> RoleUser { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
