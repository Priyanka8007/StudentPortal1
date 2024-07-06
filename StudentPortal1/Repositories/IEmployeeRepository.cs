using Microsoft.AspNetCore.Mvc;
using StudentPortal1.Models.Domain;

namespace StudentPortal1.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(Guid id);
        Task<Employee> AddAsync(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task<Employee> DeleteAsync(Guid id);
        //Task<IActionResult> GetByDesignation(string designation);

       
    }
}
