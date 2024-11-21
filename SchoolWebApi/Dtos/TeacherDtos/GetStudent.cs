namespace SchoolWebApi.Dtos.TeacherDtos
{
    public class GetStudent
    {
        public string? CourseName { get; set; }

        public string SName { get; set; } = null!;

        public int StudentNumber { get; set; }
    }
}
