using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Business.Abstract;
using Project.Core.Extensions;
using Project.Models;

namespace Project.Controllers
{
    [Authorize(Roles ="Instructor")]
    public class InstructorController : Controller
    {
        IInstructorService _instructorService;
        ICourseService _courseService;
        IUserService _userService;
        public InstructorController(IInstructorService instructorService,ICourseService courseService, IUserService userService)
        {
            _instructorService = instructorService;
            _courseService = courseService;
            _userService = userService;
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
                InstructorID = course.InstructorID,
            };
            ViewData["Instructors"] = _userService.GetByRole("Instructor");
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
                var editedCourse = new CourseToUpdateDto
                {
                    ID = id,
                    Title = _courseDto.Title,
                    Description = _courseDto.Description,
                    InstructorID = _course.InstructorID,
                    Category = _courseDto.Category,
                    ImageFile = _courseDto.ImageFile,
                    /* courseDto image ile Course image data type farklı */
                };
                await _courseService.UpdateCourseAsync(editedCourse);
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


