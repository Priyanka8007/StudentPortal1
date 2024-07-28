namespace StudentPortal1.Models.Dtos
{
    public class PayrollDto
    {
         public Guid PayrollId { get; set; }
        public Guid EmployeeId { get; set; }
        public decimal BaseSalary { get; set; }
        public int DaysWorked { get; set; }
        public decimal PerformanceBonus { get; set; }
        public decimal Deductions { get; set; }
        public decimal TotalSalary { get; set; }
        public DateTime PayDate { get; set; }
    }

}
