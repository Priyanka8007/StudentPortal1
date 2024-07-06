using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentPortal1.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPortal1.Controllers
{
    public class Dashboard1Controller : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public Dashboard1Controller(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ShowChart()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            var studentDto = students.Select(s => new { name = s.Name, age = s.Age });
            return Json(studentDto);
        }
    }
}
