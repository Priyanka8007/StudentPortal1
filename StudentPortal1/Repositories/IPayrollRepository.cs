using StudentPortal1.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPortal1.Repositories
{
    public interface IPayrollRepository
    {
        Task<IEnumerable<Payroll>> GetAllPayrollsAsync();
        Task<Payroll> GetPayrollByIdAsync(Guid id);
        Task AddPayrollAsync(Payroll payroll);
        Task UpdatePayrollAsync(Payroll payroll);
        Task DeletePayrollAsync(Guid id);
        Task<bool> PayrollExistsAsync(Guid id);
        Task<Payroll> GetPayrollByEmployeeIdAsync(Guid empId);
    }
}
