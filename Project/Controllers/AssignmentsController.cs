using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Business.Abstract;
using Project.Models;

namespace Project.Controllers
{
    public class AssignmentsController : Controller
    {
        IAssignmentService _assignmentService;
        public AssignmentsController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }
        [Authorize(Roles ="Admin,Instructor")]
        [HttpPost]
        public ActionResult CreateAssignment(Assignment assignment)
        {
            _assignmentService.CreateAssignment(assignment);
            return RedirectToAction($"{assignment.CourseID}","Course");
        }

        [Authorize(Roles = "Admin,Instructor")]
        [HttpPost]
        public ActionResult DeleteAssignment(string assignmentId,string courseId)
        {
            _assignmentService.DeleteAssignment(Guid.Parse(assignmentId));
            return RedirectToAction($"{courseId}", "Course");
        }
    }
}
