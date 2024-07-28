using StudentPortal1.Models.Domain;
using StudentPortal1.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;
using iText.IO.Font.Constants;
using iText.Kernel.Font;

namespace StudentPortal1.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly IPayrollRepository _payrollRepository;

        public PayrollService(IPayrollRepository payrollRepository)
        {
            _payrollRepository = payrollRepository;
        }

        public async Task<IEnumerable<Payroll>> GetAllPayrollsAsync()
        {
            return await _payrollRepository.GetAllPayrollsAsync();
        }

        public async Task<Payroll> GetPayrollByIdAsync(Guid id)
        {
            return await _payrollRepository.GetPayrollByIdAsync(id);
        }

        public async Task CreatePayrollAsync(Payroll payroll)
        {
            await _payrollRepository.AddPayrollAsync(payroll);
        }

        public async Task UpdatePayrollAsync(Payroll payroll)
        {
            await _payrollRepository.UpdatePayrollAsync(payroll);
        }

        public async Task DeletePayrollAsync(Guid id)
        {
            await _payrollRepository.DeletePayrollAsync(id);
        }

        public async Task<bool> PayrollExistsAsync(Guid id)
        {
            return await _payrollRepository.PayrollExistsAsync(id);
        }

        public decimal CalculateSalary(decimal baseSalary, int daysWorked, decimal performanceBonus, decimal deductions)
        {
            decimal dailyRate = baseSalary / 30; // Assuming 30 days in a month
            return (dailyRate * daysWorked) + performanceBonus - deductions;
        }



        //public byte[] GeneratePayslip(Employee employee, Payroll payroll)
        //{
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        // Create a PDF writer using the memory stream
        //        using (PdfWriter writer = new PdfWriter(memoryStream))
        //        {
        //            // Initialize PDF document
        //            PdfDocument pdf = new PdfDocument(writer);
        //            Document document = new Document(pdf);

        //            // Use Courier font instead of Helvetica
        //            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.COURIER);
        //            document.SetFont(font);

        //            // Add content to the document
        //            document.Add(new Paragraph("Payslip Details").SetFont(font));
        //            document.Add(new Paragraph($"Employee: {employee.EmpName}").SetFont(font));
        //            document.Add(new Paragraph($"Total Salary: {payroll.TotalSalary}").SetFont(font));

        //            // Close the document
        //            document.Close();
        //        }

        //        return memoryStream.ToArray();
        //    }

        
    }
}
