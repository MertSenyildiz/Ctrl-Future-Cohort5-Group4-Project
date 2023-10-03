using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Business.Abstract;
using Project.Core.Extensions;
using Project.Models;

namespace Project.Controllers
{
    public class CoursesController : Controller
    {
        ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
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
        public IActionResult Index()
        {
            var course = _courseService.GetAllCourses();
            return View(course);
        }
    }
}
