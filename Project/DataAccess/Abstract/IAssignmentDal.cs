using Project.Core.DataAccess;
using Project.Models;

namespace Project.DataAccess.Abstract
{
    public interface IAssignmentDal : IEntityRepository<Assignment>
    {
        List<AssignmentWithAllDetails> GetWithDetailsByCourse(Guid courseId);
        AssignmentWithAllDetails GetWithDetails(Guid assignmentId);
        List<AssignmentWithAllDetails> GetAllWithDetails();
    }
}