using AutoMapper;
using SchoolWebApi.Dtos.studentDtos;
using SchoolWebApi.Dtos.TeacherDtos;
using SchoolWebApi.Models;

namespace SchoolWebApi.Profiles
{
    public class CreateProfile : Profile
    {   
        public CreateProfile()
        {
            //student
            // post
            CreateMap<StudentPostDto, Student>();

            // get
            CreateMap<Course, AllCourseGetDto>()
                .ForMember(dest => dest.TName, opt => opt.MapFrom( src => src.Teacher!.TName))
                .ForMember(dest => dest.TEmail, opt => opt.MapFrom(src => src.Teacher!.TEmail))
                .ForMember(dest => dest.TOffice, opt => opt.MapFrom(src => src.Teacher!.TOffice));

            CreateMap<SelectClass, MyCourseDto>()
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.CourseName))
                .ForMember(dest => dest.TName, opt => opt.MapFrom(src => src.Course.Teacher!.TName));
           
            //Teacher
            // post
            CreateMap<TeacherPostDto, Teacher>(); 
            CreateMap<CoursePostDto, Course>();
            //get
            CreateMap<SelectClass, GetStudent>()
                .ForMember(dest => dest.SName, opt => opt.MapFrom(src => src.Student.SName));
     
        }
    }
}
