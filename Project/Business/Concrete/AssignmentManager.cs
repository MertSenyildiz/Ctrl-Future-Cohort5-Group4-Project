using Project.Business.Abstract;
using Project.DataAccess.Abstract;
using Project.DataAccess.Concrete;
using Project.Models;

namespace Project.Business.Concrete
{
    public class AssignmentManager : IAssignmentService
    {
        private readonly IAssignmentDal _assignmentDal;

        public AssignmentManager(IAssignmentDal assignmentDal)
        {
            _assignmentDal = assignmentDal;
        }

        public void CreateAssignment(Assignment assignment)
        {
            _assignmentDal.Add(assignment);
        }

        public void UpdateAssignment(Assignment assignment)
        {
            _assignmentDal.Update(assignment);
        }

        public void DeleteAssignment(Guid assignmentId)
        {
            var assigment = _assignmentDal.Get(x => x.ID == assignmentId);
            _assignmentDal.Delete(assigment);
        }

        public Assignment GetAssignmentById(Guid assignmentId)
        {

            return _assignmentDal.Get(x => x.ID == assignmentId);
        }

        public List<Assignment> GetAllAssignments()
        {
            return _assignmentDal.GetAll();
        }

        public List<Assignment> GetAssignmentsByCourse(Guid courseId)
        {
            return _assignmentDal.GetAll(x => x.ID == courseId);
        }
    }
}
