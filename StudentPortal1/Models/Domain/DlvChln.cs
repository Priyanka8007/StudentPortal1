using StudentPortal1.Models.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DlvChln
{
    [Key, Column(Order = 0)]
    public int Challanno { get; set; }  // Primary key for DlvChln

    [Key, Column(Order = 1)]
    public int Challansrno { get; set; }  // Sequential number for the challan

    [Required]
    public int Pono { get; set; }  // Foreign key part 1

    [Required]
    public int Posrno { get; set; }  // Foreign key part 2

    [MaxLength(50)]
    public string Itemcode { get; set; }  // Item code

    public decimal Qty { get; set; }  // Quantity of items in the challan, changed to decimal

    // Navigation property for the foreign key relationship with PoDtl
    [ForeignKey("Pono, Posrno")]
    public PoDtl PoDtl { get; set; }
}
