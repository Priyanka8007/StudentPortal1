using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentPortal1.Models.Domain;
using StudentPortal1.Models.Dtos;
using StudentPortal1.Repositories;
using Syncfusion.EJ2.FileManager.Base;

namespace StudentPortal1.Controllers
{
   // [ServiceFilter(typeof(EmployeeActionFilter))]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
      

        public EmployeeController(
           IEmployeeRepository employeeRepository,
           ICityRepository cityRepository,
           ICountryRepository countryRepository,
           IStateRepository stateRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _stateRepository = stateRepository;
            _mapper = mapper;

        }

        [HttpGet]
       // [ServiceFilter(typeof(EmployeeActionFilter))]
        public async Task<IActionResult> GetEmployees(int page = 1, int pageSize = 3)
        {
            var employees = await _employeeRepository.GetAllAsync();
            var totalCount = employees.Count();

            var paginatedEmployees = employees.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var employeeDtos = _mapper.Map<List<EmployeeDto>>(paginatedEmployees);

            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return Json(new { data = employeeDtos, totalPages });
            return RedirectToAction("Index", "Payroll");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

       // [ServiceFilter(typeof(EmployeeActionFilter))]
        public async Task<IActionResult> Create()
        {
            var employeeDto = new EmployeeDto();
            await PopulateDropdowns(employeeDto);
            return View(employeeDto);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(employeeDto);
                employee.EmpId = Guid.NewGuid();
                var country = await _countryRepository.GetCountryNameByCountryIdAsync(employeeDto.CountryId);
                var state = await _stateRepository.GetStstenameBystateIdAsync(employeeDto.StateId);
                var city = await _cityRepository.GetByIdAsync(employeeDto.CityId);

                // Assign state and city to employee
                //employee.State = state;
                //employee.City = city;

                // Set navigation properties
                employee.Country = country;
                employee.State = state;
                 employee.City = city;
                await _employeeRepository.AddAsync(employee);
                return RedirectToAction(nameof(Index));
            }

            await PopulateDropdowns(employeeDto);
            return View(employeeDto);
        }

        private async Task PopulateDropdowns(EmployeeDto employeeDto)
        {
            ViewBag.Countries = new SelectList(await _countryRepository.GetAllCountriesAsync(), "CountryId", "Name");
            ViewBag.States = new SelectList(await _stateRepository.GetAllStatesAsync(), "StateId", "Name");
            ViewBag.Cities = new SelectList(await _cityRepository.GetAllCitiesAsync(), "CityId", "Name");
        }

        [HttpGet]
        public async Task<IActionResult> GetStatesByCountry(int countryId)
        {
            var states = await _stateRepository.GetStatesByCountryIdAsync(countryId);
            var stateDtos = _mapper.Map<IEnumerable<StateDto>>(states);
            return Json(stateDtos);
        }

        [HttpGet("Employee/GetCitiesByState/{stateId}")]
        public async Task<IActionResult> GetCitiesByState(int stateId)
        {
            if (stateId == 0)
            {
                return BadRequest("Invalid stateId");
            }

            var cities = await _cityRepository.GetCitiesByStateIdAsync(stateId);
            if (cities == null || !cities.Any())
            {
                return NotFound("No cities found for the specified stateId.");
            }

            var cityDtos = cities.Select(c => new { c.CityId, c.Name });
            return Json(cityDtos);
        }


    }
}
