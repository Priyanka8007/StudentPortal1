using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentPortal1.Models.Domain
{
    public class EmployeeCertification
    {
        [Key]
        public Guid EmployeeCertificationId { get; set; }

        [ForeignKey("EmployeeSkill")]
        public Guid EmployeeSkillId { get; set; }
        public EmployeeSkill EmployeeSkill { get; set; }

        [ForeignKey("Employee")]
        public Guid EmpId { get; set; }
        public Employee Employee { get; set; }

        public string CertificationName { get; set; }
        public DateTime DateObtained { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string IssuingAuthority { get; set; }

        
       
    }

}
