using Project.Models;

namespace Project.Business.Abstract
{
    public interface ICourseService
    {
        void CreateCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(Guid courseId);
        Course GetCourseById(Guid courseId);
        List<Course> GetAllCourses();
        List<Course> GetCoursesByInstructor(Guid instructorId);
    }
}
