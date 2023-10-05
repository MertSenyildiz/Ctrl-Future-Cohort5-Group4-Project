using Project.Data.Enum;

namespace Project.Models
{
    public class CourseToUpdateDto
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public CourseCategory Category { get; set; }
        public Guid InstructorID { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
