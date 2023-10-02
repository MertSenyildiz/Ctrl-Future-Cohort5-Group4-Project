using Project.Models;

namespace Project.Business.Abstract
{
    public interface IAssignmentService
    {
        void CreateAssignment(Assignment assignment);
        void UpdateAssignment(Assignment assignment);
        void DeleteAssignment(Guid assignmentId);
        Assignment GetAssignmentById(Guid assignmentId);
        List<Assignment> GetAllAssignments();
        List<Assignment> GetAssignmentsByCourse(Guid courseId);
    }
}
