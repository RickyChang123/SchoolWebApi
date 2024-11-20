using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolWebApi.Dtos.TeacherDtos;
using SchoolWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly SchoolSystemContext _dbContext;

        private readonly IMapper _mapper;

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

        // 獲得
        [HttpGet("getStudent")]
        public IEnumerable<GetStudent> GetStudent(string TName)
        {
            List<SelectClass> studentSelect =  _dbContext.SelectClasses
                .Include(s => s.Student).Include(t => t.Course.Teacher).ToList();

            List<GetStudent> result = [];

            foreach (var teacher in studentSelect)
            {
                if (teacher.Course.Teacher?.TName == TName)
                {
                    var studentName = teacher.Student.SName;

                    var mystudent = _mapper.Map<GetStudent>(studentName);

                    result.Add(mystudent);

                }
            }

            return result;
        }
        
        // 老師註冊
        [HttpPost("TeacherPost")]
        public void Post([FromBody] TeacherPostDto value)
        {
            Teacher teacher = _mapper.Map<Teacher>(value);

            var random = new Random();

            int randomDay = random.Next(1, 31);

            teacher.Tid = Guid.NewGuid();

            teacher.TStart = DateTime.Now.AddDays(- randomDay);

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

            var random = new Random();

            int randomDay = random.Next(1, 31);

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
