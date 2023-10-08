using Project.Data.Enum;

namespace Project.Models
{
    public class EnrollmentWithAllDetails:Enrollment
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
