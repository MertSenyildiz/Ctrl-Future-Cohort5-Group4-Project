using Project.Business.Abstract;
using Project.Core.Business;
using Project.Core.Helpers.FileHelpers;
using Project.DataAccess.Abstract;
using Project.Models;

namespace Project.Business.Concrete
{
    public class AdministrationManager : IAdminService
    {
        private readonly IUserDal _userDal;
        IUserService _userService;
        private readonly ICourseDal _courseDal;
        IFileHelper _fileSaver;
        public AdministrationManager(IUserDal userDal, ICourseDal courseDal, IUserService userService, IFileHelper fileSaver)
        {
            _userDal = userDal;
            _courseDal = courseDal;
            _fileSaver = fileSaver;
            _userService = userService;
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }
        public List<UserToShowDto> GetAllUsers()
        {
            return _userService.GetAll();
        }
        public User GetById(Guid id)
        {
            var user = _userDal.Get(u=>u.ID == id);
            return user;
        }

        public User GetByMail(string mail)
        {
            var user = _userDal.Get(u => u.Email == mail);
            return user;
        }

        public User GetByRefreshToken(string refreshToken)
        {
            var user = _userDal.Get(u => u.RefreshToken == refreshToken);
            return user;
        }

        public void Update(User user)
        {
            _userDal.Update(user);
        }
        public void DeleteUser (Guid userId)
        {
            var user = _userDal.Get(x => x.ID == userId);
            
            _userDal.Delete(user);
        }
        /* Impletion of Course methods */
        public async Task CreateCourse(CourseToAddDto course)
        {
            var courseToAdd = new Course
            {
                ID = Guid.NewGuid(),
                Title = course.Title,
                InstructorID = course.InstructorID,
                Description=course.Description,
                Category=course.Category,
                EnrollmentCount=0,
                ImageUrl=await _fileSaver.SaveFileAsync(course.ImageFile),
            };
            _courseDal.Add(courseToAdd);
        }

        public void UpdateCourse(Course course)
        {
            _courseDal.Update(course);
        }

        public void DeleteCourse(Guid courseId)
        {
            var course = _courseDal.Get(x => x.ID == courseId);
            _courseDal.Delete(course);
        }

        public Course GetCourseById(Guid courseId)
        {
            return _courseDal.Get(x =>x.ID == courseId);
        }

        public List<Course> GetAllCourses()
        {
            return _courseDal.GetAll();
        }

        public List<Course> GetCoursesByInstructor(Guid instructorId)
        {
            return _courseDal.GetAll(x => x.ID == instructorId);
        }


    }
}
