using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentPortal1.Models.Domain
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EmpId { get; set; }

       
        public string EmpName { get; set; }

      
        public int Age { get; set; }

       
        public string Gender { get; set; }

        
        public string Designation { get; set; }

       
        [EmailAddress]
        public string Email { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        [ForeignKey("State")]
        public int StateId { get; set; }
        public State State { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }
        public City City { get; set; }

        [Required]
        public string Pincode { get; set; }

       // public ICollection<EmployeeSkill> EmployeeSkills { get; set; }
       // public ICollection<EmployeeCertification> EmployeeCertifications { get; set; }
    }
}
