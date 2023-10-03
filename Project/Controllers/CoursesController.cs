using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Business.Abstract;
using Project.Core.Extensions;
using Project.Models;
using System.Security.Claims;

namespace Project.Controllers
{
    public class CoursesController : Controller
    {
        ICourseService _courseService;
        IEnrollmentService _enrollmentService;
        public CoursesController(ICourseService courseService,IEnrollmentService enrollmentService)
        {
            _courseService = courseService;
            _enrollmentService = enrollmentService;
        }
        [Authorize(Roles ="Student")]
        public ActionResult Create() {
            
            return View();
        
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CourseToAddDto course)
        {
            var id = HttpContext.User.Claims("id")[0];
            course.InstructorID = Guid.Parse(id);
            await _courseService.CreateCourseAsync(course);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteCourse(string courseId)
        {
            _courseService.DeleteCourse(Guid.Parse(courseId));
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var courses = _courseService.GetAllCourses();
            if(HttpContext.User.Claims("id").Any())
            {
                if (HttpContext.User.Claims(ClaimTypes.Role)[0]=="Student")
                {
                    var alreadyEnrolledCourses = _courseService.GetCoursesByUser(Guid.Parse(HttpContext.User.Claims("id")[0])).Select(c=>c.ID);
                    courses = courses.Where(c => !alreadyEnrolledCourses.Contains(c.ID)).ToList();
                }
                
            }
            return View(courses);
        }

        [Authorize]
        public IActionResult MyCourses()
        {
            ViewData["Title"] = HttpContext.User.Claims("username")[0] + "'s Courses";
            var role = HttpContext.User.Claims(ClaimTypes.Role)[0];
            IEnumerable<Course> courses=null;
            switch (role)
            {
                case "Instructor":
                    var insID = HttpContext.User.Claims("id")[0];
                    courses = _courseService.GetCoursesByInstructor(Guid.Parse(insID));
                    break;
                case "Admin":
                    courses=_courseService.GetAllCourses();
                    break;
                default:
                    var uID = HttpContext.User.Claims("id")[0];
                    courses = _courseService.GetCoursesByUser(Guid.Parse(uID));
                    break;
            }
            return View(courses);
            
        }

        
    }
}
