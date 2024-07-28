using System.ComponentModel.DataAnnotations;

namespace StudentPortal1.Models.Dtos
{
    public class EmployeeDto
    {
        public Guid EmpId { get; set; }

       
        [StringLength(100)]
        public string EmpName { get; set; }

        
        public int Age { get; set; }

       
        public string Gender { get; set; }

        
        public string Designation { get; set; }
        public string Email { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Display(Name = "State")]
        public int StateId { get; set; }

        [Display(Name = "City")]
        public int CityId { get; set; }

        public string Pincode { get; set; }
    }
}
