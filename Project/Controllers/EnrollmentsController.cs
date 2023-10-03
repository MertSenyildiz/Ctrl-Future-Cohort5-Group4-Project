using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Project.Business.Abstract;
using Project.Core.Extensions;

namespace Project.Controllers
{
    public class EnrollmentsController : Controller
    {
        IEnrollmentService _enrollmentService;
        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [Authorize(Roles ="Student")]
        [HttpPost]
        public IActionResult Enroll(string courseId)
        {
            var userId = HttpContext.User.Claims("id")[0];
            _enrollmentService.Enroll(Guid.Parse(userId), Guid.Parse(courseId));
            return RedirectToAction("MyCourses","Courses");
        }
        [Authorize(Roles = "Student,Admin,Instructor")]
        [HttpPost]
        public IActionResult Disenroll(string courseId)
        {
            var userId = HttpContext.User.Claims("id")[0];
            _enrollmentService.Disenroll(Guid.Parse(userId),Guid.Parse(courseId));
            return RedirectToAction("MyCourses", "Courses");
        }
    }
}
