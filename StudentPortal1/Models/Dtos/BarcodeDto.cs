using System.ComponentModel.DataAnnotations;

namespace StudentPortal1.Models.Dtos
{
    public class BarcodeDto
    {
        [Key]
        public int Id { get; set; }  // Primary key for the person record

        [Required]
        [StringLength(100)]
        public string Name { get; set; }  // The person's name

        [Range(1, 120)]
        public int Age { get; set; }  // The person's age (valid range from 1 to 120)

        [Required]
        [StringLength(20)]
        public string Barcd { get; set; }  // The barcode associated with the person

        [StringLength(250)]
        public string Address { get; set; }  // The person's address

        [StringLength(50)]
        public string PhoneNumber { get; set; }  // The person's phone number
    }
}
