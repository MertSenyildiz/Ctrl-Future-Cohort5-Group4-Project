using Project.Models;

namespace Project.Business.Abstract
{
    public interface IAssignmentService
    {
        void CreateAssignment(Assignment assignment);
        void UpdateAssignment(Assignment assignment);
        void DeleteAssignment(Guid assignmentId);

        [Obsolete("GetAssignmentById is deprecated, please use GetAssignmentWithDetails instead.")]
        Assignment GetAssignmentById(Guid assignmentId);
        AssignmentWithAllDetails GetAssignmentWithDetails(Guid assignmentId);


        [Obsolete("GetAllAssignments is deprecated, please use GetAllAssignmentsWithDetails instead.")]
        List<Assignment> GetAllAssignments();
        List<AssignmentWithAllDetails> GetAllAssignmentsWithDetails();


        [Obsolete("GetAssignmentsByCourse is deprecated, please use GetAllAssignmentsWithDetailsByCourse instead.")]
        List<Assignment> GetAssignmentsByCourse(Guid courseId);
        List<AssignmentWithAllDetails> GetAllAssignmentsWithDetailsByCourse(Guid courseId);
    }
}
