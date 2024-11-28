using StudentPortal1.Models.Dtos;

namespace StudentPortal1.Models.Domain
{
    public class SplitQuantityRequest
    {
        public List<RowData> Rows { get; set; }
        public decimal SplitQuantity { get; set; }
    }

}