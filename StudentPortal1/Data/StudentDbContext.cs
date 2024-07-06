using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentPortal1.Models.Domain;
using StudentPortal1.Models.Dtos;

namespace StudentPortal1.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { set; get; }
        public DbSet<Person>Persons { set; get; }

    }

}
