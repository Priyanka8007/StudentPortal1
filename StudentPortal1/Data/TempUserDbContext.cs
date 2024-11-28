using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentPortal1.Models.Domain;

namespace StudentPortal1.Data
{
    public class TempUserDbContext : DbContext
    {
        public TempUserDbContext(DbContextOptions<TempUserDbContext> options) : base(options)
        {

        }
        public DbSet<TempUser> tempUsers { get; set; }
    }
}
    
 