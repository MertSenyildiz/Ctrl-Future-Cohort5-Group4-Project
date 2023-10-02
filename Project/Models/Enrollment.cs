namespace Project.Models
{
    public class Enrollment
    {
        public Guid ID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public Guid CourseID { get; set;}
        public Guid UserID { get; set; }

    }
}
