using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class UserRegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
        [Required]
        [PasswordPropertyText]
        [Compare("Password", ErrorMessage = "Emails mismatch")]
        public string PasswordVerify { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
