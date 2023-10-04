using Project.Models;

namespace Project.Business.Abstract
{
    public interface IAdminService
    {
        /* Authorazation for courses */
        Task CreateCourse(CourseToAddDto course);

        void UpdateCourse(Course course);

        void DeleteCourse(Guid courseId);
        
        Course GetCourseById(Guid courseId);
        
        List<Course> GetAllCourses();

        List<Course> GetCoursesByInstructor(Guid instructorId);

        /* ------------------------------------ */
        /* Authorazation over students */
        void Add(User user);
        
        void Update(User user);
        
        void DeleteUser(Guid userId);

        public List<User> GetAllUsers();

        User GetById(Guid id);

        User GetByMail(string mail);

        User GetByRefreshToken(string refreshToken);

        /* ----------------------------------------- */
    }
}
