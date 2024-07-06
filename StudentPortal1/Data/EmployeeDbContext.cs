using Microsoft.EntityFrameworkCore;
using StudentPortal1.Models.Domain;

namespace StudentPortal1.Data
{
    public class EmployeeDbContext :DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { set; get; }
    }
}
