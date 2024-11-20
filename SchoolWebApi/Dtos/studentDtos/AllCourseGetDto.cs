using System.ComponentModel.DataAnnotations;

namespace SchoolWebApi.Dtos.studentDtos
    {
    public class AllCourseGetDto
    {
        public string? CourseName { get; set; }

        public string? CourseDescription { get; set; }

        public string Credits { get; set; } = null!;

        public string? Location { get; set; }

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }

        public string? Day { get; set; }

        public string TName { get; set; } = null!; 

        [EmailAddress]
        public  string? TEmail { get; set; }

        public  string? TOffice { get; set; }
    }
}
