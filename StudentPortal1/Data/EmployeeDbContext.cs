using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentPortal1.Models;
using StudentPortal1.Models.Domain;
using StudentPortal1.Models.Dtos;

namespace StudentPortal1.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }
        // public DbSet<Product> Products { get; set; }
        //  public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }



        public DbSet<Employee> Employees { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Payroll>(entity =>
            {
                entity.Property(e => e.BaseSalary).HasPrecision(18, 2);
                entity.Property(e => e.PerformanceBonus).HasPrecision(18, 2);
                entity.Property(e => e.Deductions).HasPrecision(18, 2);
                entity.Property(e => e.TotalSalary).HasPrecision(18, 2);
            });
            // Configure relationships
            modelBuilder.Entity<State>()
                .HasOne(s => s.Country)
                .WithMany(c => c.States)
                .HasForeignKey(s => s.CountryId);

            modelBuilder.Entity<City>()
                .HasOne(c => c.State)
                .WithMany(s => s.Cities)
                .HasForeignKey(c => c.StateId);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Country)
                .WithMany()
                .HasForeignKey(e => e.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.State)
                .WithMany()
                .HasForeignKey(e => e.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.City)
                .WithMany()
                .HasForeignKey(e => e.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data
            modelBuilder.Entity<Country>().HasData(
                new Country { CountryId = 1, Name = "USA" },
                new Country { CountryId = 2, Name = "Canada" }
            );

            modelBuilder.Entity<State>().HasData(
                new State { StateId = 1, Name = "California", CountryId = 1 },
                new State { StateId = 2, Name = "Texas", CountryId = 1 },
                new State { StateId = 3, Name = "Ontario", CountryId = 2 },
                new State { StateId = 4, Name = "Quebec", CountryId = 2 }
            );

            modelBuilder.Entity<City>().HasData(
                new City { CityId = 1, Name = "Los Angeles", StateId = 1 },
                new City { CityId = 2, Name = "San Francisco", StateId = 1 },
                new City { CityId = 3, Name = "Houston", StateId = 2 },
                new City { CityId = 4, Name = "Dallas", StateId = 2 },
                new City { CityId = 5, Name = "Toronto", StateId = 3 },
                new City { CityId = 6, Name = "Montreal", StateId = 4 }
            );

            // Mark SelectListGroup as keyless
            modelBuilder.Entity<SelectListGroup>().HasNoKey();



        }
    }
}
