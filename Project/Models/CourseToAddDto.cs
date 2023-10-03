namespace Project.Models
{
    public class CourseToAddDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public Guid InstructorID { get; set; }
        public IFormFile? ImageFile { get; set; }

    }
}
