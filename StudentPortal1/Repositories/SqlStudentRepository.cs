using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentPortal1.Data;
using StudentPortal1.Models.Domain;

namespace StudentPortal1.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _context;

        public SqlStudentRepository(StudentDbContext context)
        {
            _context = context;
        }
        public async Task<Student?> GetStudentByIdAsync(Guid studentId)
        {
            return await _context.Students.FindAsync(studentId);
        }
      
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }

        //public async Task<Student?> DeleteStudentAsync(Guid id)
        //{
        //    var existingStudent = await _context.Students.FirstOrDefaultAsync(x => x.StdId == id);
        //    if (existingStudent == null)
        //    {
        //        return null;
        //    }
        //     _context.Students.Remove(existingStudent);
        //    await _context.SaveChangesAsync();
        //    return existingStudent;
        //}


        public async Task DeleteStudentAsync(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Student>> GetStudentsByAgeAsync(int age)
    {
        return await _context.Students.FromSqlInterpolated($"EXEC GetStudentsByAge {age}").ToListAsync();
    }
    public async  Task<Student?> UpdateStudentAsync(Guid id, Student student)
        {
            var existingStudent = await _context.Students.FirstOrDefaultAsync(x => x.StdId == id);
            if (existingStudent == null)
            {
                return null;
            }
            existingStudent.Name = student.Name;
            existingStudent.Age = student.Age;
            await _context.SaveChangesAsync();
            return existingStudent;
        }

      
    }

}