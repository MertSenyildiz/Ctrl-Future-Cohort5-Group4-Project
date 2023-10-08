using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Business.Abstract;
using Project.Core.Extensions;
using Project.Models;

namespace Project.Controllers
{
    public class InstructorController : Controller
    {
        IInstructorService _instructorService;
        ICourseService _courseService;

        public InstructorController(IInstructorService instructorService,ICourseService courseService)
        {
            _instructorService = instructorService;
            _courseService = courseService;
        }
        [Authorize(Roles = "Instructor")]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Instructor")]
        public IActionResult Index()
        {
            var id = Guid.Parse(HttpContext.User.Claims("id")[0]);
            var courses = _courseService.GetCoursesWithDetailsByInstructor(id); /* Bringing all the table from database and drop into your table */

            return View(courses);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseToAddDto course)
        {
            var id = HttpContext.User.Claims("id")[0];
            course.InstructorID = Guid.Parse(id);
            await _courseService.CreateCourseAsync(course);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            Course course = _instructorService.GetCourseById(id);
            return View(course);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var course = _instructorService.GetCourseById(id);
            if (course == null) return View("Error");
            var courseDTO = new CourseToAddDto
            {
                Title = course.Title,
                Description = course.Description,
                Category = course.Category,
            };
            return View(courseDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, CourseToAddDto _courseDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return RedirectToAction("Index");
            }
            var _course = _instructorService.GetCourseById(id);
            if (_course != null)
            {
                //var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);
                var editedCourse = new Course
                {
                    ID = id,
                    Title = _courseDto.Title,
                    Description = _courseDto.Description,
                    InstructorID = _course.InstructorID,
                    Category = _courseDto.Category,
                    EnrollmentCount = _course.EnrollmentCount,
                    ImageUrl = _course.ImageUrl,
                    /* courseDto image ile Course image data type farklı */
                };
                _instructorService.UpdateCourse(editedCourse);
                return RedirectToAction("Index");
            }
            else
            {
                var courseDTO = new CourseToAddDto
                {
                    Title = _course.Title,
                    Description = _course.Description,
                    InstructorID = _course.InstructorID,
                    Category = _course.Category,
                };

                return View(courseDTO);
            }
        }
    }
}


