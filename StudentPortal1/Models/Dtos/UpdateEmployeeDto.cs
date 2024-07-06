namespace StudentPortal1.Models.Dtos
{
    public class UpdateEmployeeDto
    {
        public Guid EmpId { get; set; }

        public string EmpName { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public string Designation { set; get; }

        //create dropdown for this from database
        public string Country { set; get; }

        public string State { set; get; }

        public string City { set; get; }

        public string EmailId { set; get; }

        public string Pincode { set; get; }

    }
}
