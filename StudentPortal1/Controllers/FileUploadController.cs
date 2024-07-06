// Controllers/FileUploadController.cs
using Microsoft.AspNetCore.Mvc;
using StudentPortal1.Models.Domain;
using System.IO;
using System.Threading.Tasks;
using StudentPortal1.Models.Domain;

namespace StudentPortal1.Controllers
{
    public class FileUploadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upload(FileUpload model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null && model.File.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadFiles", model.File.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(stream);
                    }

                    ViewBag.Message = "File uploaded successfully!";
                }
                else
                {
                    ViewBag.Message = "Please select a file.";
                }
            }
            else
            {
                ViewBag.Message = "Model is not valid.";
            }

            return View("Upload");
        }
    }
}
