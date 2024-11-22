using System.ComponentModel.DataAnnotations;

namespace SchoolWebApi.Parameters
{
    public class StudentLoginParamter
    {
        [Required]
        public string? Email{ get; set; }

        [Required]
        public string? Password { get; set; }

    }
}
