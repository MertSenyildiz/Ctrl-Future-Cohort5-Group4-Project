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
        [Authorize(Roles ="Instructor")]
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
        public IActionResult Index(string? nameFilter, int? categoryFilter)
        {
            var courses = _courseService.GetAllCoursesWithDetailByFilters(nameFilter,categoryFilter);
            if(nameFilter is not null)
            {
                ViewData["nameFilter"] = nameFilter;
            }
            if(categoryFilter is not null)
            {
                ViewData["categoryFilter"] = categoryFilter;
            }
            if(HttpContext.User.Claims("id").Any())
            {
                if (HttpContext.User.Claims(ClaimTypes.Role)[0]=="Student")
                {
                    var alreadyEnrolledCourses = _courseService.GetCoursesWithDetailsByUser(Guid.Parse(HttpContext.User.Claims("id")[0])).Select(c=>c.ID);
                    courses = courses.Where(c => !alreadyEnrolledCourses.Contains(c.ID)).ToList();
                }
                
            }
            return View(courses);
        }
        [Route("/Course/{courseId}")]
        public IActionResult IndexCourse(string courseId)
        {
            var course = _courseService.GetCourseWithDetail(Guid.Parse(courseId));
            if (HttpContext.User.Claims("id").Any())
            {
                if (HttpContext.User.Claims(ClaimTypes.Role)[0] == "Admin" 
                    || (HttpContext.User.Claims(ClaimTypes.Role)[0] == "Instructor" 
                    && course.InstructorID== Guid.Parse(HttpContext.User.Claims("id")[0])))
                {
                    ViewData["Users"] =_enrollmentService.GetWithDetailsByCourseId(Guid.Parse(courseId));
                }
            }
            ViewData["Assignments"] = _enrollmentService.GetWithDetailsByCourseId(Guid.Parse(courseId));
            return View(course);
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
                    courses = _courseService.GetCoursesWithDetailsByInstructor(Guid.Parse(insID));
                    break;
                case "Admin":
                    courses=_courseService.GetAllCoursesWithDetail();
                    break;
                default:
                    var uID = HttpContext.User.Claims("id")[0];
                    courses = _courseService.GetCoursesWithDetailsByUser(Guid.Parse(uID));
                    break;
            }
            return View(courses);
        }

        
    }
}
