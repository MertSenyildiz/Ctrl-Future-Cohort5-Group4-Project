using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    public class InstructorController : Controller
    {

        [Authorize(Roles = "Student")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
