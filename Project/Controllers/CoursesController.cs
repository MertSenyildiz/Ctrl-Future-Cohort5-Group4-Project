using Microsoft.AspNetCore.Mvc;
using Project.Business.Abstract;

namespace Project.Controllers
{
    public class CoursesController : Controller
    {
        ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public ActionResult Create() {
            
            return View();
        
        }
        public IActionResult Index()
        {
            var course = _courseService.GetAllCourses();
            return View(course);
        }
    }
}
