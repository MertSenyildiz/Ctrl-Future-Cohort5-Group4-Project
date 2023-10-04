using System.ComponentModel.DataAnnotations.Schema;
using Project.Data.Enum;
namespace Project.Models
{
    public class Course
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid InstructorID { get; set; }
        public CourseCategory Category { get; set; }

        public int EnrollmentCount { get; set; }

        public string ImageUrl { get; set; }
    }
}
