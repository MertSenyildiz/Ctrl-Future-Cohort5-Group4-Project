using Project.Models;

namespace Project.Business.Abstract
{
    public interface ICourseService
    {
        Task CreateCourseAsync(CourseToAddDto course);
        void UpdateCourse(Course course);
        void DeleteCourse(Guid courseId);
        Course GetCourseById(Guid courseId);
        List<Course> GetAllCourses();
        List<Course> GetCoursesByInstructor(Guid instructorId);
        List<Course> GetCoursesByUser(Guid userId);

        void IncrementEnrollmentCout(Guid courseId);
        void DecrementEnrollmentCout(Guid courseId);
    }
}
