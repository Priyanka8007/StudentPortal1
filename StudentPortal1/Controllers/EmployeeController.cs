using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentPortal1.Models.Domain;
using StudentPortal1.Models.Dtos;
using StudentPortal1.Repositories;
using StudentPortal1.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPortal1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        // GET: Employees
      
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllAsync();
            var employeeDTOs = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return View(employeeDTOs);
        }

        // GET: Employees/Details/{id}
        public async Task<IActionResult> Details(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var employeeDTO = _mapper.Map<EmployeeDto>(employee);
            return View(employeeDTO);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
       [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(AddEmployeeDto createEmployeeDto)
{
    if (ModelState.IsValid)
    {
        var employee = _mapper.Map<Employee>(createEmployeeDto);
        await _employeeService.AddAsync(employee);
        return Json(new { message = "Employee added successfully." });
    }

    return Json(new { message = "Error adding employee.", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
}



        // GET: Employees/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var updateEmployeeDto = _mapper.Map<UpdateEmployeeDto>(employee);
            return View(updateEmployeeDto);
        }

        // POST: Employees/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            if (id != updateEmployeeDto.EmpId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(updateEmployeeDto);
                await _employeeService.UpdateAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(updateEmployeeDto);
        }

        // GET: Employees/Delete/{id}
        public async Task<IActionResult> Delete(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var employeeDTO = _mapper.Map<EmployeeDto>(employee);
            return View(employeeDTO);
        }

        // POST: Employees/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _employeeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //// GET: Employees/ByDesignation/{designation}
        //[HttpGet("Employees/ByDesignation/{designation}")]
        //public async Task<IActionResult> GetByDesignation(string designation)
        //{
        //    var employees = await _employeeService.GetByDesignation(designation);
        //    var employeeDTOs = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        //    return View(employeeDTOs);
        //}
    }
}
