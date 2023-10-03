using Project.Business.Abstract;
using Project.Core.Business;
using Project.DataAccess.Abstract;
using Project.Models;

namespace Project.Business.Concrete
{
    public class EnrollmentManager:IEnrollmentService
    {
        IEnrollmentDal _enrollmentDal;
        ICourseService _courseService;
        public EnrollmentManager(IEnrollmentDal enrollmentDal, ICourseService courseService)
        {
            _enrollmentDal = enrollmentDal;
            _courseService = courseService;
        }

        public void Enroll(Guid userID, Guid courseID)
        {
            var result = BusinessRules.Run(IsUserAlreadyEnrolled(userID,courseID));
            if (result)
            {
                throw new Exception("User's already been taking this course");
            }
            var enrollment = new Enrollment
            {
                ID = Guid.NewGuid(),
                CourseID = courseID,
                UserID = userID,
                EnrollmentDate = DateTime.Now,
            };
            _enrollmentDal.Add(enrollment);
            _courseService.IncrementEnrollmentCout(courseID);
        }

        public void Disenroll(Guid userID, Guid courseID)
        {
            var enrollment=GetByUserAndCourse(userID,courseID);
            if(enrollment != null)
            {
                _courseService.DecrementEnrollmentCout(enrollment.CourseID);
                _enrollmentDal.Delete(enrollment);
            }
        }
        private bool IsUserAlreadyEnrolled(Guid userID, Guid courseID)
        {
            var enrollment = GetByUserAndCourse(userID,courseID);
            if(enrollment != null)
            {
                return true;
            }
            return false;
        }
        public Enrollment GetByUserAndCourse(Guid userID, Guid courseID)
        {
            return _enrollmentDal.Get(e => e.UserID == userID && e.CourseID == courseID);
        }

        public List<Enrollment> GetByCourseId(Guid courseID)
        {
            return _enrollmentDal.GetAll(e => e.CourseID == courseID);
        }
    }
}
