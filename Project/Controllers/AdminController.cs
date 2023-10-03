using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Business.Abstract;
using Project.Core.Extensions;
using Project.Models;

namespace Project.Controllers
{ 
    public class AdminController : Controller
    {
        
        IAdminService _adminService;
        
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        //[Authorize(Roles ="Admin")]
        public ActionResult Create() {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseToAddDto course)
        {
            var id = HttpContext.User.Claims("id")[0];
            course.InstructorID = Guid.Parse(id);
            await _adminService.CreateCourse(course);
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var course = _adminService.GetAllCourses();
            return View(course);
        }
    }
}
