using StudentPortal1.Models.Domain;
using StudentPortal1.Models.Dtos;

namespace StudentPortal1.Repositories
{
    public interface IEmployeeRepository 
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(Guid empId);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Employee>> GetPaginatedEmployeesAsync(int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync();
        // Employee GetById(int id);
     
    }
}
