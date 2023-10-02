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
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Enroll()
        {
            var courseID = Guid.Parse(Request.Form["courseID"]);
            _enrollmentService.Enroll(Guid.Parse(HttpContext.User.Claims("id")[0]), courseID);
            return Ok();
        }
    }
}
