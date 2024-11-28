using System.ComponentModel.DataAnnotations;

namespace StudentPortal1.Models.Domain
{
    public class DesignEngineer
    {
        [Key]
        public int DeId { get; set; } // Primary key

        [Required]
        [StringLength(100)]
        public string Name { get; set; } // Engineer's name

        [Required]
        [StringLength(100)]
        public string Department { get; set; } // Department name

        [Required]
        public DateTime AssignedDate { get; set; } = DateTime.Now; // Assignment date

        public DateTime? DeassignedDate { get; set; } // De-assignment date
    }
}
