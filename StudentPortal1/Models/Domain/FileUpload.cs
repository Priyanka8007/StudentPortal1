using System.ComponentModel.DataAnnotations;

namespace StudentPortal1.Models.Domain
{
    public class FileUpload
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
