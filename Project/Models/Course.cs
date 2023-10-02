using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Course
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid InstructorID { get; set; }
        public string Category { get; set; }

        public string CategoryName { get; set; }

        public int EnrollmentCount { get; set; }

        public string ImageUrl { get; set; }
    }
}
