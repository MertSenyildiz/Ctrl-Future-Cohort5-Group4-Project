using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class UserRegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Emails mismatch")]
        public string PasswordVerify { get; set; }
        public string Username { get; set; }
    }
}
