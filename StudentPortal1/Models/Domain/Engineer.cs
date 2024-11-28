using System.ComponentModel.DataAnnotations;

namespace StudentPortal1.Models.Domain
{
    public class Engineer
    {
        [Key]
        public int EId { get; set; } // Primary key

        [Required]
        [StringLength(100)]
        public string EName { get; set; } // Engineer's name

        [Required]
        [StringLength(100)]
        public string EDepartment { get; set; } // Department name

        public bool IsAssigned { get; set; } = false; // Assignment status
    }
}
