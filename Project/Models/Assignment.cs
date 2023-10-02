namespace Project.Models
{
    public class Assignment
    {
        public Guid ID { get; set; }
        public Guid CourseID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
