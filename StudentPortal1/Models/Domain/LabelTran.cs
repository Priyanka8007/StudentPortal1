using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace StudentPortal1.Models.Domain
{
    public class LabelTran
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Identity column
        public int LabelSrNo { get; set; }

        [Required]
        [StringLength(36)] // Matches VARCHAR(36) in SQL
        public string SessionId { get; set; }

        [Required]
        public int ChallanNo { get; set; }

        [Required]
        public int ChallanSrNo { get; set; }

        [Required]
        public int PONo { get; set; }

        [Required]
        public int POSrNo { get; set; }

        [Required]
        [StringLength(50)] // Matches NVARCHAR(50) in SQL
        public string ItemCode { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")] // Matches DECIMAL(18,2) in SQL
        public decimal Qty { get; set; }

        [Required]
        public int LabelNo { get; set; }
    }

}