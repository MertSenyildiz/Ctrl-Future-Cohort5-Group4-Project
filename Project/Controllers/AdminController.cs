using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Business.Abstract;
using Project.Core.Extensions;
using Project.Models;

namespace Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        
        IAdminService _adminService;
        IUserService _userService;
        ICourseService _courseService;
        public AdminController(IAdminService adminService, IUserService userService, ICourseService courseService)
        {
            _adminService = adminService;
            _userService = userService;
            _courseService = courseService;
        }
        [Authorize(Roles ="Admin")]
        public ActionResult Create() {
            
            ViewData["Instructors"] = _userService.GetByRole("Instructor");
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index( )
        {
            var courses =  _courseService.GetAllCoursesWithDetail(); /* Bringing all the table from database and drop into your table */

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
            ViewData["Instructors"] = _userService.GetByRole("Instructor");
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

        [Authorize(Roles = "Admin")]
        public IActionResult UserManager()
        {
            var users = _adminService.GetAllUsers( );
            return View(users);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserEdit(Guid id)
        {
            var user = _adminService.GetById(id);
            if(user == null) return View("Error");
            var userDTO = new UserToShowDto
            {
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
            };
            return View(userDTO);
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(Guid id, UserToShowDto userDTO)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("","Failed to edit user");
                return RedirectToAction("Index");
            }
            var _user = _adminService.GetById(id);
            if(_user != null)
            {
                //var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);
                var editedUser = new User
                {
                    ID = _user.ID,
                    Username = userDTO.Username,
                    Email = userDTO.Email,
                    PasswordSalt = _user.PasswordSalt,
                    PasswordHash = _user.PasswordHash,
                    Role = userDTO.Role,
                    RefreshToken = _user.RefreshToken, 
                    /* courseDto image ile Course image data type farklı */
                };
                _adminService.Update(editedUser);
                return  RedirectToAction("Index");
            }
            else    
                return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> UserDelete(Guid id)
        {
            var user = _adminService.GetById(id);
            if(user == null) return View("Error");
            else
            {   
                var _user = new UserToShowDto
                {
                    ID = user.ID,
                    Username = user.Username,
                    Email = user.Email,
                    Role = user.Role,
                };
                
                return View(_user);
            }
        }

        [HttpPost, ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
              var userDetails = _adminService.GetById(id);
            if(userDetails == null) return View("Error");

            _adminService.DeleteUser(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UserDetail(Guid id)
        {
            var user = _adminService.GetById(id);
            if(user == null) return View("Error");
            var userDTO = new UserToShowDto
            {
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
            };
            return View(userDTO);
        }

    }
}
