using AutoMapper;
using StudentPortal1.Models.Domain;
using StudentPortal1.Models.Dtos;

namespace StudentPortal1.Mappings
{
    public class AutoMapperProfiles : Profile
    {
       
        public AutoMapperProfiles()
        {
            CreateMap<BarcodeDto, Barcode>();
            // Mapping between Student and StudentDto
            CreateMap<Student, StudentDto>().ReverseMap();

            // Mapping between AddStudentRequestDto and Student
            CreateMap<AddStudentDto, Student>();

            // Mapping between UpdateStudentRequestDto and Student
            CreateMap<UpdateDtos, Student>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            // CreateMap<Employee, EmployeeDto>().ReverseMap();

            CreateMap<AddEmployeeDto, Employee>().ReverseMap();
            //  CreateMap< EmployeeDto,>().ReverseMap();
            CreateMap<City, CityDto>();
            CreateMap<CityDto, City>();
            CreateMap<State, StateDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Payroll, PayrollDto>().ReverseMap();
            CreateMap< PayrollDto, Payroll>().ReverseMap();
            CreateMap<ChallanSplitResultDto, DlvChln>().ReverseMap();
        }
    }
}
