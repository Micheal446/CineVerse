using System.ComponentModel.DataAnnotations;

namespace CineVerse.models
{
    public class RegisterDto
    {
        [Required(ErrorMessage =" wronge email")]
        [EmailAddress(ErrorMessage =" wrong incorrect email sentex")]

        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage ="wronge password ")]
        [MinLength(8,ErrorMessage ="mini length ")]
        public string Password { get; set; }


    }
}
