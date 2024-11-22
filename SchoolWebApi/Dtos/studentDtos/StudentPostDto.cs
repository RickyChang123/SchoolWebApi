using System.ComponentModel.DataAnnotations;

namespace SchoolWebApi.Dtos.studentDtos
{
    public class StudentPostDto
    {
        [Required]
        public string SName { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage ="Not EmailAddress")]
        public string SEmail { get; set; } = null!;

        [Required(ErrorMessage = "Password is require.")]
        [StringLength(15, MinimumLength = 6,ErrorMessage ="Length must between 6 to 15.")]
        public string SPassword { get; set; } = null!;

    }
}
