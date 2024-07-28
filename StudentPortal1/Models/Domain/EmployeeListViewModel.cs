using StudentPortal1.Models.Dtos;

namespace StudentPortal1.Models.Domain
{
    public class EmployeeListViewModel
    {
        public IEnumerable<EmployeeDto> Employees { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

}
