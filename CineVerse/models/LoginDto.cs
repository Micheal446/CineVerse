using System.ComponentModel.DataAnnotations;

namespace CineVerse.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "you must email!")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة!")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "must password!")]
        public string Password { get; set; } = string.Empty;
    }
}