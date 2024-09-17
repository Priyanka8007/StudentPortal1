using ConsumeWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ConsumeWebApi.Controllers
{
    
    public class StudentController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:10439");
        private List<Student>? studentlist;
        private readonly HttpClient _httpClient;
        public StudentController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Student> students = new List<Student>();
            HttpResponseMessage responce=_httpClient.GetAsync(_httpClient.BaseAddress + "/" +
                "Student/Index").Result;
            if (responce.IsSuccessStatusCode)
            {
                string data=responce.Content.ReadAsStringAsync().Result;
                studentlist=JsonConvert.DeserializeObject<List<Student>>(data);
            }
            return View(studentlist);
        }
    }
}
