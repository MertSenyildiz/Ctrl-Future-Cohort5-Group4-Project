using Project.Models;

namespace Project.Business.Abstract
{
    public interface IEnrollmentService
    {
        void Enroll(Guid userID,Guid courseID);
        void Disenroll(Guid userID,Guid courseID);

        Enrollment GetByUserAndCourse(Guid userID,Guid courseID);


        [Obsolete("GetByCourseId is deprecated, please use GetWithDetailsByCourseId instead.")]
        List<Enrollment> GetByCourseId(Guid courseID);
        List<EnrollmentWithAllDetails> GetWithDetailsByCourseId(Guid courseID);

    }
}
