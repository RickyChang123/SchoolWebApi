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
            CreateMap<StudentCoursePostDto, SelectClass>();

            // get
            CreateMap<Course, AllCourseGetDto>()
                .ForMember(dest => dest.TName, opt => opt.MapFrom( src => src.Teacher!.TName))
                .ForMember(dest => dest.TEmail, opt => opt.MapFrom(src => src.Teacher!.TEmail))
                .ForMember(dest => dest.TOffice, opt => opt.MapFrom(src => src.Teacher!.TOffice));

            CreateMap<SelectClass, MyCourseDto>()
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.CourseName))
                .ForMember(dest => dest.TName, opt => opt.MapFrom(src => src.Course.Teacher!.TName))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Course.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.Course.EndTime))
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Course.Day));
           
            //Teacher
            // post
            CreateMap<TeacherPostDto, Teacher>(); 
            CreateMap<CoursePostDto, Course>();
        }
    }
}
