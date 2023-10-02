namespace Project.Business.Abstract
{
    public interface IEnrollmentService
    {
        void Enroll(Guid userID,Guid courseID);
        void Disenroll(Guid ID);

    }
}
