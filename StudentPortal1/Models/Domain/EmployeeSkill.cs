using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentPortal1.Models.Domain
{
    public class EmployeeSkill
    {
        [Key]
        public Guid EmployeeSkillId { get; set; }

        [ForeignKey("Employee")]
        public Guid EmpId { get; set; }
        public Employee Employee { get; set; }

        public string SkillName { get; set; }
        public int ProficiencyLevel { get; set; } // e.g., 1-5 scale

        // Navigation property for EmployeeCertification relationship
        public ICollection<EmployeeCertification> EmployeeCertifications { get; set; }
    }

}
