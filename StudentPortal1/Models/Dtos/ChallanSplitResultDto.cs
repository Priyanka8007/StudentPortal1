namespace StudentPortal1.Models.Dtos
{
    public class ChallanSplitResultDto
    {
        public int Challanno { get; set; }
        public int Challansrno { get; set; }
        public int Pono { get; set; }
        public int Posrno { get; set; }
      
        public  string Itemcode { get; set; }  // Item code
        public decimal Qty { get; set; }
        public decimal LabelQty { get; set; }
        public int labelno { get; set; }
    }

}
