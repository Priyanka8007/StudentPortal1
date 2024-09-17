using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentPortal1.Models;
using System.Reflection.Emit;

namespace StudentPortal1.Data
{
    public class StudentAuthDbContext : IdentityDbContext
    {
        public StudentAuthDbContext(DbContextOptions<StudentAuthDbContext> options) : base(options)
        {

        }
        public DbSet<UserActivity> UserActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "1caf4338-a2fe-4c16-a63b-ff423caf02b3";
            var writerRoleId = "1b58dfc3-ee99-4ba9-9786-3e13c5554ac3";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id=readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                 new IdentityRole
                {
                    Id=writerRoleId,
                    ConcurrencyStamp=writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
