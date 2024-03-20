using AutoMapper;
using StudentPortal1.Models.Domain;
using StudentPortal1.Models.Dtos;

namespace StudentPortal1.Mappings
{
    public class AutoMapperProfiles : Profile
    {
       
        public AutoMapperProfiles()
        {
            // Mapping between Student and StudentDto
            CreateMap<Student, StudentDto>().ReverseMap();

            // Mapping between AddStudentRequestDto and Student
            CreateMap<AddStudentDto, Student>();

            // Mapping between UpdateStudentRequestDto and Student
            CreateMap<UpdateDtos, Student>();
          //  CreateMap<DeleteDtos, Student>();

        }
    }
}
