using Microsoft.AspNetCore.Mvc;
using StudentPortal1.Models.Domain;

namespace StudentPortal1.Controllers
{
    public class PersonController : Controller
    {
       
        [HttpGet]
        public IActionResult GetPerson()
        {
            var person = new Person { Name = "Alice", CurrentDate = DateTime.Now };
            return Json(person);

        }
    }
}
