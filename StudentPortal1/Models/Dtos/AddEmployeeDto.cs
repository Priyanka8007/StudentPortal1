using System.ComponentModel.DataAnnotations;

namespace StudentPortal1.Models.Dtos
{
    public class AddEmployeeDto
    {
        public Guid EmpId { get; set; }

        [Required]
        [StringLength(100)]
        public string EmpName { get; set; }

        [Range(18, 65)]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [StringLength(100)]
        public string Designation { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        public int StateId { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        [EmailAddress]
        public string EmailId { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string Pincode { get; set; }
    }
}
