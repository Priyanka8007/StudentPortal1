using StudentPortal1.Models.Domain;
using StudentPortal1.Models.Dtos;

namespace StudentPortal1.Repositories
{
 

        public interface IStudentRepository
        {
            Task<Student?> GetStudentByIdAsync(Guid studentId);   
            Task<List<Student>> GetAllStudentsAsync();
            Task<Student> AddStudentAsync(Student student);
            Task<Student?> UpdateStudentAsync(Guid studentId, Student student);
        Task DeleteStudentAsync(Student student); // Ad
        Task<List<Student>> GetStudentsByAgeAsync(int age);




    }


    
}
