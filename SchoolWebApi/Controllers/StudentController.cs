using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SchoolWebApi.Dtos.studentDtos;
using SchoolWebApi.Models;
using System.ComponentModel.Design;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly SchoolSystemContext _dbContext;

        private readonly IMapper _mapper;

        //初始化變數
        public StudentController(SchoolSystemContext schoolSystemContext, IMapper mapper)
        {
            _dbContext = schoolSystemContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "NO", "VALUE" };
        }

        //獲得全部課程
        [HttpGet("studentGet")]
        public IEnumerable<AllCourseGetDto> GetClass()
        {
            List<AllCourseGetDto> result = [];
         
            List<Course> courses = _dbContext.Courses.Include( c => c.Teacher).ToList();

            result = _mapper.Map<List<AllCourseGetDto>>(courses);
            
            return result;
        }

        //已選課程
        [HttpGet("GetYourCourse")]
        public IEnumerable<MyCourseDto> YourCourse(string StudentName)
        {
            List<SelectClass> myCourse = _dbContext.SelectClasses.Include(c => c.Course)
                .Include(t => t.Course.Teacher)
                .Include(n => n.Student)
                .ToList();

            List<MyCourseDto> result = [];

            foreach (var name in myCourse)
            {
                if (name.Student.SName == StudentName)
                {
                    var course = _mapper.Map<MyCourseDto>(name);
                    result.Add(course);
                }
            }

            return result;
        }

        // 學生註冊
        [HttpPost("studentRegister")]
        public IActionResult SPost([FromBody] StudentPostDto value)
        {

            Student student = _mapper.Map<Student>(value);

            var random = new Random();

            int randomDay = random.Next(1, 31);

            student.Sid = Guid.NewGuid();
            student.SStart = DateTime.Now.AddDays(-randomDay);
            student.SEnd = DateTime.Now;

            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();

            return Ok(student);

        }

        // 選課
        [HttpPost("SelectClass")]
        public  IActionResult SelectCourse([FromBody] StudentCoursePostDto value)
        {

            Guid course_id = _dbContext.Courses.Where(c => c.CourseName == value.CourseName)
                .Select( c => c.Cid )
                .FirstOrDefault();
            
            Guid student_id = _dbContext.Students.Where(s => s.SName == value.SName)
                .Select(s=>s.Sid )
                .FirstOrDefault();

            List<SelectClass> selectedCourses = _dbContext.SelectClasses
                .Where(sc => sc.StudentId == student_id)
                .ToList();

            bool isAlreadySelect = selectedCourses.Any(src => src.CourseId == course_id);

            if (isAlreadySelect)
            {
                return BadRequest("Already select this course.");
            }

            var random = new Random();

            int randomDay = random.Next(1, 31);

            var select = new SelectClass
            {
                Eid = Guid.NewGuid(),
                StudentId = student_id,
                CourseId = course_id,
                EStart = DateTime.Now.AddDays(-randomDay),
                EEnd = DateTime.Now,

            };

            _dbContext.SelectClasses.Add(select);
            _dbContext.SaveChanges();

            return Ok(select);
        
        }

    }
}
