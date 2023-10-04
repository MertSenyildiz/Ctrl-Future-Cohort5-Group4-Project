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
        //[Authorize(Roles ="Student")]
        public ActionResult Create() {
            
            return View();
        }
        public IActionResult Index( )
        {
            var courses =  _adminService.GetAllCourses( ); /* Bringing all the table from database and drop into your table */

            return View(courses);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseToAddDto course)
        {
            //var id = HttpContext.User.Claims("id")[0];
           // course.InstructorID = Guid.Parse(id);
            await _adminService.CreateCourse(course);
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Detail(Guid id)
        {
            Course course = _adminService.GetCourseById(id);
            return View(course);
        }
        
        public async Task<IActionResult> Edit(Guid id)
        {
            var course = _adminService.GetCourseById(id);
            if(course == null) return View("Error");
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
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("","Failed to edit club");
                return RedirectToAction("Index");
            }
            var _course = _adminService.GetCourseById(id);
            if(_course != null)
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
                _adminService.UpdateCourse(editedCourse);
                return  RedirectToAction("Index");
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
        public async Task<IActionResult> Delete(Guid ID)
        {
            var courseDetails = _adminService.GetCourseById(ID);
            if(courseDetails == null) return View("Error");
            return View(courseDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(Guid id)
        {   
            var courseDetails = _adminService.GetCourseById(id);
            if(courseDetails == null) return View("Error");

            _adminService.DeleteCourse(id);
            return RedirectToAction("Index");
        }
    
    }
}
