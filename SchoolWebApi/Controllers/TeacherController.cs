using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolWebApi.Dtos.studentDtos;
using SchoolWebApi.Dtos.TeacherDtos;
using SchoolWebApi.Models;
using System.Diagnostics.Metrics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace SchoolWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly SchoolSystemContext _dbContext;

        private readonly IMapper _mapper;

        public static Random random = new Random();

        public int randomDay = random.Next(1, 31);

        // 初始化變數
        public TeacherController(SchoolSystemContext schoolSystemContext, IMapper mapper)
        {
            _dbContext = schoolSystemContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "NO", "VALUE" };
        }

        // 課程、學生人數、名子
        [HttpGet("getStudent")]
        public IEnumerable<GetStudent> GetStudent(string TName)
        {
            List<SelectClass> studentSelect =  _dbContext.SelectClasses
                .Include(s => s.Student).Include(t => t.Course.Teacher).Where(t => t.Course.Teacher!.TName == TName).ToList();

            List<GetStudent> result = studentSelect.GroupBy(c => c.Course.CourseName).Select(g => new GetStudent
            {
                CourseName = g.Key,
                SName = string.Join(',', g.Select(s => s.Student.SName)),
                StudentNumber = g.Count(),

            }).ToList();

            return result;

        }
        
        // 老師註冊
        [HttpPost("TeacherPost")]
        public void Post([FromBody] TeacherPostDto value)
        {
            Teacher teacher = _mapper.Map<Teacher>(value);

            teacher.Tid = Guid.NewGuid();

            teacher.TStart = DateTime.Now.AddDays(-randomDay);

            teacher.TEnd = DateTime.Now;

            _dbContext.Add(teacher);  
            _dbContext.SaveChanges();

        }

       // 加入課程
       [HttpPost("TeacherPost/AddCourse")]
       public IActionResult CoursePost([FromBody] CoursePostDto value)
       {
            Course course = _mapper.Map<Course>(value);

            course.Cid = Guid.NewGuid();

            course.CStart = DateTime.Now.AddDays(-randomDay);

            course.CEnd = DateTime.Now;

            Guid? Teacher_id = _dbContext.Teachers
                .Where(t => t.TName == value.TName)
                .Select(t => t.Tid)
                .FirstOrDefault();
            
            if (Teacher_id != null)
            {
                course.TeacherId = Teacher_id;
            }
           
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();

            return Ok(course);
       } 
    }
}
