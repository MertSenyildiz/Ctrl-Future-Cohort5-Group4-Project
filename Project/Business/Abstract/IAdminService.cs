using Project.Models;

namespace Project.Business.Abstract
{
    public interface IAdminService
    {
        Task CreateCourse(CourseToAddDto course);

        void UpdateCourse(Course course);

        void DeleteCourse(Guid courseId);
        
        Course GetCourseById(Guid courseId);
        
        List<Assignment> GetAllAssignments();
        
        List<Course> GetAllCourses();
        
        List<Course> GetCoursesByInstructor(Guid instructorId);

    }
}
