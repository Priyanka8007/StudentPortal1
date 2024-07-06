namespace StudentPortal1.Models.Dtos
{
    public class EmployeeDto
    {
        public Guid EmpId { get; set; }
        public string EmpName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string EmailId { get; set; }
        public string Pincode { get; set; }
    }
}
