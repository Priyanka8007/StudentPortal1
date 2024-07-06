using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal1.Models.Domain;
using StudentPortal1.Models.Dtos;
using StudentPortal1.Repositories;

namespace StudentPortal1.Controllers
{
   //Authorize]
    public class StudentController : Controller

    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }
        [HttpGet ("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var getRegionDomain = await _studentRepository.GetStudentByIdAsync(id);
            if (getRegionDomain == null)
            {
                return NotFound();
            }

            //Mapping Using Mapper
            var getRegionDto = _mapper.Map<StudentDto>(getRegionDomain);

            return View(getRegionDto);
        }


       
        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string searchString, int page = 1)
        {
            const int pageSize = 3; // Number of records to display per page


            var students = await _studentRepository.GetAllStudentsAsync();

            var paginatedStudents = students.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var studentDtos = _mapper.Map<List<StudentDto>>(paginatedStudents);

            var totalPages = (int)Math.Ceiling(students.Count / (double)pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(studentDtos);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddStudentDto studentDto)
        {

            var student = _mapper.Map<Student>(studentDto);
            await _studentRepository.AddStudentAsync(student);

            return View(studentDto);
        }
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, UpdateDtos updateDtos)
        {
            var studentDomainModel = _mapper.Map<Student>(updateDtos);

            studentDomainModel = await _studentRepository.UpdateStudentAsync(id, studentDomainModel);
            if (studentDomainModel == null)
            {
                return NotFound();
            }
            //Mapping from Mapper

            var regionDto = _mapper.Map<StudentDto>(studentDomainModel);
            return View(regionDto);
        }

        public IActionResult Delete()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var studentToDelete = await _studentRepository.GetStudentByIdAsync(id);
            if (studentToDelete == null)
            {
                return NotFound();
            }

            await _studentRepository.DeleteStudentAsync(studentToDelete);

            return RedirectToAction(nameof(Index)); // Redirect to the index page after successful deletion
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentsByAge(int age)
        {
            var studentEntities = await _studentRepository.GetStudentsByAgeAsync(age);
            var studentDtos = _mapper.Map<List<StudentDto>>(studentEntities);

            return View(studentDtos);
        }


    }
}
   