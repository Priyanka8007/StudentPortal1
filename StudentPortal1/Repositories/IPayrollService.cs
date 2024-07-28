using StudentPortal1.Models.Domain;
using StudentPortal1.Models.Dtos;

namespace StudentPortal1.Repositories
{
    public interface IPayrollService
    {
        Task<IEnumerable<Payroll>> GetAllPayrollsAsync();
        Task<Payroll> GetPayrollByIdAsync(Guid id);
        Task CreatePayrollAsync(Payroll payroll);
        Task UpdatePayrollAsync(Payroll payroll);
        Task DeletePayrollAsync(Guid id);
        Task<bool> PayrollExistsAsync(Guid id);
        decimal CalculateSalary(decimal baseSalary, int daysWorked, decimal performanceBonus, decimal deductions);
       // byte[] GeneratePayslip(Employee employee, Payroll payroll);
    }
}
