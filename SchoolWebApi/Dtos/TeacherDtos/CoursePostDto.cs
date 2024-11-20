namespace SchoolWebApi.Dtos.TeacherDtos
{
    public class CoursePostDto
    {
        public string TName { get; set; } = null!;
        public string? CourseName { get; set; }

        public string? CourseDescription { get; set; }

        public int Credits { get; set; }

        public string? Location { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public string? Day { get; set; }
    }
}
