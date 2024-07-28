using Microsoft.EntityFrameworkCore;
using StudentPortal1.Data;
using StudentPortal1.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPortal1.Repositories
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly EmployeeDbContext _context;

        public PayrollRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payroll>> GetAllPayrollsAsync()
        {
            return await _context.Payrolls.Include(p => p.Employee).ToListAsync();
        }

        public async Task<Payroll> GetPayrollByIdAsync(Guid id)
        {
            return await _context.Payrolls.Include(p => p.Employee)
                                         .FirstOrDefaultAsync(p => p.PayrollId == id);
        }

        public async Task AddPayrollAsync(Payroll payroll)
        {
            _context.Payrolls.Add(payroll);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePayrollAsync(Payroll payroll)
        {
            _context.Payrolls.Update(payroll);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePayrollAsync(Guid id)
        {
            var payroll = await _context.Payrolls.FindAsync(id);
            if (payroll != null)
            {
                _context.Payrolls.Remove(payroll);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> PayrollExistsAsync(Guid id)
        {
            return await _context.Payrolls.AnyAsync(e => e.PayrollId == id);


        }
        //public async Task<Payroll> GetPayrollByEmployeeIdAsync(Guid employeeId)
        //{
        //    return await _context.Payrolls.Include(p => p.Employee)
        //                                  .FirstOrDefaultAsync(p => p.EmployeeId == employeeId);
        //}
        public async Task<Payroll> GetPayrollByEmployeeIdAsync(Guid employeeId)
        {
            return await _context.Payrolls.Include(p => p.Employee)
                                          .FirstOrDefaultAsync(p => p.EmployeeId == employeeId);
        }
    }
}
