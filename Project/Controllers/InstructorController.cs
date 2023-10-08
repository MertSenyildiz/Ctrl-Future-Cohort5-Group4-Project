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
            // Implement the logic to add a course 
            // Redirect to appropriate view or action
            _courseService.CreateCourseAsync(courseToAdd);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteCourse(Guid courseId)
        {
            // Implement the logic to delete a course
            // Redirect to appropriate view or action
            _courseService.DeleteCourse(courseId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteAssignment(Guid assignmentId)
        {
            // Implement the logic to delete an assignment
            // Redirect to appropriate view or action
            _assignmentService.DeleteAssignment(assignmentId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CreateAssignment(Assignment assignment)
        {
            // Implement the logic to create an assignment
            // Redirect to appropriate view or action
            _assignmentService.CreateAssignment(assignment);
            return RedirectToAction("Index");
        }

    }
}
