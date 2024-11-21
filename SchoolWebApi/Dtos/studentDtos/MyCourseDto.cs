namespace SchoolWebApi.Dtos.studentDtos
{
    public class MyCourseDto
    {
        public string? CourseName { get; set; }
        public string TName { get; set; } = null!;

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }

        public string? Day { get; set; }
    }
}
