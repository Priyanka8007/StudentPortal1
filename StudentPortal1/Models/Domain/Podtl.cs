namespace StudentPortal1.Models.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PoDtl
    {
        [Key, Column(Order = 0)]
        public int Pono { get; set; }  // Primary key part 1

        [Key, Column(Order = 1)]
        public int Posrno { get; set; }  // Primary key part 2

        [MaxLength(50)]
        public string Cono { get; set; }  // Company number

        [MaxLength(50)]
        public string MfgUnit { get; set; }  // Manufacturing unit

        // Navigation property for related DlvChln entries
        public ICollection<DlvChln> DlvChlns { get; set; }
    }

}
