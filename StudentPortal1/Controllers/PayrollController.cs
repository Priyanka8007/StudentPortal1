using Microsoft.AspNetCore.Mvc;
using StudentPortal1.Models.Domain;
using StudentPortal1.Models.Dtos;
using StudentPortal1.Services;
using System;
using System.Threading.Tasks;
using AutoMapper;
using StudentPortal1.Repositories;
using System.Collections.Generic;

namespace StudentPortal1.Controllers
{
    public class PayrollController : Controller
    {
        private readonly IPayrollService _payrollService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IPayrollRepository _payrollRepository;

        public PayrollController(IPayrollService payrollService, IEmployeeRepository employeeRepository, IMapper mapper,IPayrollRepository payrollRepository)
        {
            _payrollService = payrollService;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _payrollRepository = payrollRepository;
        }

        // GET: Payroll
        public async Task<IActionResult> Index()
        {
            var payrollRecords = await _payrollService.GetAllPayrollsAsync();
            var payrollDtos = _mapper.Map<IEnumerable<PayrollDto>>(payrollRecords);
            return View(payrollDtos);
        }

        // GET: Payroll/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var payroll = await _payrollService.GetPayrollByIdAsync(id);
            if (payroll == null)
            {
                return NotFound();
            }
            var payrollDto = _mapper.Map<PayrollDto>(payroll);
            return View(payrollDto);
        }

        // GET: Payroll/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Employees = await _employeeRepository.GetAllAsync();
            return View();
        }

        // POST: Payroll/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PayrollDto payrollDto)
        {
            if (ModelState.IsValid)
            {
                var employee = await _employeeRepository.GetByIdAsync(payrollDto.EmployeeId);
                if (employee == null)
                {
                    ModelState.AddModelError("EmployeeId", "Invalid Employee ID.");
                    ViewBag.Employees = await _employeeRepository.GetAllAsync();
                    return View(payrollDto);
                }

                var payroll = _mapper.Map<Payroll>(payrollDto);
                payroll.TotalSalary = _payrollService.CalculateSalary(payroll.BaseSalary, payroll.DaysWorked, payroll.PerformanceBonus, payroll.Deductions);
                await _payrollService.CreatePayrollAsync(payroll);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Employees = await _employeeRepository.GetAllAsync();
            return View(payrollDto);
        }


        // GET: Payroll/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var payroll = await _payrollService.GetPayrollByIdAsync(id);
            if (payroll == null)
            {
                return NotFound();
            }
            var payrollDto = _mapper.Map<PayrollDto>(payroll);
            ViewBag.Employees = await _employeeRepository.GetAllAsync();
            return View(payrollDto);
        }

        // POST: Payroll/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PayrollDto payrollDto)
        {
            if (id != payrollDto.PayrollId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var payroll = _mapper.Map<Payroll>(payrollDto);
                    payroll.TotalSalary = _payrollService.CalculateSalary(payroll.BaseSalary, payroll.DaysWorked, payroll.PerformanceBonus, payroll.Deductions);
                    await _payrollService.UpdatePayrollAsync(payroll);
                }
                catch (Exception)
                {
                    if (!await _payrollService.PayrollExistsAsync(id))
                    {
                        return NotFound();
                    }
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Employees = await _employeeRepository.GetAllAsync();
            return View(payrollDto);
        }

        // GET: Payroll/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var payroll = await _payrollService.GetPayrollByIdAsync(id);
            if (payroll == null)
            {
                return NotFound();
            }
            var payrollDto = _mapper.Map<PayrollDto>(payroll);
            return View(payrollDto);
        }

        // POST: Payroll/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _payrollService.DeletePayrollAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult CalculateSalary()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CalculateSalary(decimal baseSalary, int daysWorked, decimal performanceBonus, decimal deductions)
        {
            var totalSalary = _payrollService.CalculateSalary(baseSalary, daysWorked, performanceBonus, deductions);
            return Json(new { totalSalary });
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return Json(employeeDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollByEmployeeId(Guid employeeId)
        {
            var payroll = await _payrollRepository.GetPayrollByEmployeeIdAsync(employeeId);
            if (payroll == null)
            {
                return NotFound();
            }
            var payrollDto = _mapper.Map<PayrollDto>(payroll);
            return Json(payrollDto);
        }

        //[HttpPost]
        //public async Task<IActionResult> GeneratePayslip(Guid id)
        //{
        //    var payroll = await _payrollService.GetPayrollByIdAsync(id);
        //    if (payroll == null)
        //    {
        //        return NotFound();
        //    }

        //    var employee = await _employeeRepository.GetByIdAsync(payroll.EmployeeId);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    var employeeDto = _mapper.Map<EmployeeDto>(employee);
        //    var payrollDto = _mapper.Map<PayrollDto>(payroll);

        //    // Convert DTOs back to domain models if necessary
        //    var employeeDomain = _mapper.Map<Employee>(employeeDto);
        //    var payrollDomain = _mapper.Map<Payroll>(payrollDto);

        //    var payslipBytes = _payrollService.GeneratePayslip(employeeDomain, payrollDomain);

        //    return File(payslipBytes, "application/pdf", "Payslip.pdf");
        //}
    }
}
