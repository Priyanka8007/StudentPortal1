using Microsoft.EntityFrameworkCore;
using StudentPortal1.Models.Domain;

namespace StudentPortal1.Data
{
    public class EngineerDbContext : DbContext
    {
        public EngineerDbContext(DbContextOptions<EngineerDbContext> options) : base(options)
        {
        }

        public DbSet<Engineer> Engineers { get; set; } // DbSet for Engineers
        public DbSet<DesignEngineer> DesignEngineers { get; set; } // DbSet for Design Engineers

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Model configurations can go here if needed
        }
    }
}
