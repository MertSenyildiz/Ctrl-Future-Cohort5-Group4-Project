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
            var assignmentToUpdate=_assignmentDal.Get(a=>a.ID==assignment.ID);
            if(assignmentToUpdate!=null)
            {
                assignmentToUpdate.Title = assignment.Title;
                assignmentToUpdate.Description = assignment.Description;
                assignmentToUpdate.DueDate= assignment.DueDate;
                _assignmentDal.Update(assignmentToUpdate);
            }
            else
            {
                throw new Exception("No course was found to update");
            }
            
        }

        public void DeleteAssignment(Guid assignmentId)
        {
            var assigment = _assignmentDal.Get(x => x.ID == assignmentId);
            if(assigment!=null)
            {
                _assignmentDal.Delete(assigment);
            }
            
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

        public AssignmentWithAllDetails GetAssignmentWithDetails(Guid assignmentId)
        {
            return _assignmentDal.GetWithDetails(assignmentId);
        }

        public List<AssignmentWithAllDetails> GetAllAssignmentsWithDetails()
        {
            return _assignmentDal.GetAllWithDetails();
        }

        public List<AssignmentWithAllDetails> GetAllAssignmentsWithDetailsByCourse(Guid courseId)
        {
            return _assignmentDal.GetWithDetailsByCourse(courseId);
        }
    }
}
