using Project.Business.Abstract;
using Project.DataAccess.Abstract;
using Project.Models;

namespace Project.Business.Concrete
{
    public class EnrollmentManager:IEnrollmentService
    {
        IEnrollmentDal _enrollmentDal;
        public EnrollmentManager(IEnrollmentDal enrollmentDal)
        {
            _enrollmentDal = enrollmentDal;
        }

        public void Enroll(Guid userID, Guid courseID)
        {
            var enrollment = new Enrollment
            {
                ID = Guid.NewGuid(),
                CourseID = courseID,
                UserID = userID,
                EnrollmentDate = DateTime.Now,
            };
            _enrollmentDal.Add(enrollment);
        }

        public void Disenroll(Guid ID)
        {
            var enrollment=_enrollmentDal.Get(e=>e.ID == ID);
            if(enrollment != null)
            {
                _enrollmentDal.Delete(enrollment);
            }
        }
    }
}
