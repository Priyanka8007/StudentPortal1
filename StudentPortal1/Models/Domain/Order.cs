using System.ComponentModel.DataAnnotations;

namespace StudentPortal1.Models.Domain
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalValue { get; set; }
        public string ProductType { get; set; }
    }
}
