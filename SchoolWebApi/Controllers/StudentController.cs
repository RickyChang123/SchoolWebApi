using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SchoolWebApi.Dtos.studentDtos;
using SchoolWebApi.Models;
using SchoolWebApi.Parameters;
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

        public static Random random = new Random();

        public int randomDay = random.Next(1, 31);

        // 初始化變數
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

        // 獲得全部課程
        [HttpGet("AllClass")]
        public IEnumerable<AllCourseGetDto> GetClass()
        {
            List<AllCourseGetDto> result = [];
         
            List<Course> courses = _dbContext.Courses.Include( c => c.Teacher).ToList();

            result = _mapper.Map<List<AllCourseGetDto>>(courses);
            
            return result;

        }

        // 獲得已選課程
        [HttpPost("YourCourse")]
        public IEnumerable<MyCourseDto> YourCourse([FromBody] YourCourseParameter value)
        {
            string? Name = value.StudentName;

            Name = HttpContext.Session.GetString("StudentName");

            List<SelectClass> myCourse = _dbContext.SelectClasses.Include(c => c.Course)
                .Include(t => t.Course.Teacher)
                .Include(n => n.Student)
                .ToList();

            List<MyCourseDto> result = myCourse
                .Where(sn => sn.Student.SName == Name)
                .Select(value => _mapper.Map<MyCourseDto>(value)).ToList();

            return result;
        }

        // 學生註冊
        [HttpPost("StudentRegister")]
        public IActionResult SPost([FromBody] StudentPostDto value)
        {

            Student student = _mapper.Map<Student>(value);

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

            value.SName = HttpContext.Session.GetString("StudentName");

            Guid course_id = _dbContext.Courses.Where(c => c.CourseName == value.CourseName)
                .Select(c => c.Cid )
                .FirstOrDefault();
            
            Guid student_id = _dbContext.Students.Where(s => s.SName == value.SName)
                .Select(s => s.Sid)
                .FirstOrDefault();
            
            List<SelectClass> selectedCourses = _dbContext.SelectClasses
                .Where(sc => sc.StudentId == student_id)
                .ToList();

            // 課程是否重複選
            bool isAlreadySelect = selectedCourses.Any(src => src.CourseId == course_id);

            if (isAlreadySelect)
            {
                return BadRequest("Already select this course.");
            }

            SelectClass select = _mapper.Map<SelectClass>(value);

            // 自動生成
            select.Eid =Guid.NewGuid();
            select.CourseId = course_id;
            select.StudentId = student_id;  
            select.EStart = DateTime.Now.AddDays(-randomDay);
            select.EEnd = DateTime.Now;           

            _dbContext.SelectClasses.Add(select);
            _dbContext.SaveChanges();

            return Ok(select);
        }

        // 登入
        [HttpPost("Login")]
        public IActionResult StudentLogin([FromBody] StudentLoginParamter value)
        {
            Student? student = _dbContext.Students
                .Where(s => s.SEmail == value.Email & s.SPassword == value.Password)
                .SingleOrDefault();

            string? studentName = null;

            if (student?.SEmail == null || student.SPassword ==null)
            {
               return BadRequest("Login Error!!!");
            }
            else
            {
                studentName = student.SName;
                HttpContext.Session.SetString("StudentName", studentName);
            }  

            return Ok(studentName);
        }
    }
}
