using System.ComponentModel.DataAnnotations;

namespace StudentPortal1.Models.Dtos
{
    public class RegisterDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { set; get; }
    }
}
