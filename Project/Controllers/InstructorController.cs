using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Business.Abstract;
using Project.Models;

namespace Project.Controllers
{
    public class InstructorController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IAssignmentService _assignmentService;

        public InstructorController(ICourseService courseService, IAssignmentService assignmentService)
        {
            _courseService = courseService;
            _assignmentService = assignmentService;
        }


        public IActionResult Index()
        {
            // Your instructor dashboard or landing page
            return View();
        }

        [HttpPost]
        public IActionResult AddCourse(CourseToAddDto courseToAdd)
        {
            // Implement the logic to add a course using _courseService.CreateCourse(courseToAdd);
            // Redirect to appropriate view or action
            _courseService.CreateCourse(courseToAdd);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteAssignment(Guid assignmentId)
        {
            // Implement the logic to delete an assignment using _assignmentService.DeleteAssignment(assignmentId);
            // Redirect to appropriate view or action
            _assignmentService.DeleteAssignment(assignmentId);
            return RedirectToAction("Index");
        }

    }
}
