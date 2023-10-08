using Project.Business.Abstract;
using Project.Core.Helpers.FileHelpers;
using Project.DataAccess.Abstract;
using Project.Migrations;
using Project.Models;
using System;
using System.Collections.Generic;


namespace Project.Business.Concrete
{
    public class CourseManager : ICourseService
    {
        
        private readonly ICourseDal _courseDal;
        IFileHelper _fileSaver;

        public CourseManager(ICourseDal courseDal,IFileHelper fileHelper)
        {
            _courseDal = courseDal;
            _fileSaver = fileHelper;
        }

       public async Task CreateCourseAsync(CourseToAddDto course)
        {
            var courseToAdd = new Course
            {
                ID=Guid.NewGuid(),
                Title=course.Title,
                InstructorID = course.InstructorID,
                Description=course.Description,
                Category=course.Category,
                EnrollmentCount=0,
                ImageUrl=await _fileSaver.SaveFileAsync(course.ImageFile),
            };
            _courseDal.Add(courseToAdd);
        }

        public async Task UpdateCourseAsync(CourseToUpdateDto course)
        {
            var courseToUpdate = _courseDal.Get(c=>c.ID==course.ID);
            if (courseToUpdate != null)
            {
                courseToUpdate.Title = course.Title;
                courseToUpdate.Description=course.Description;
                courseToUpdate.Category=course.Category;
                courseToUpdate.InstructorID = course.InstructorID;
                if (course.ImageFile != null)
                {
                    var path = courseToUpdate.ImageUrl;
                    _fileSaver.DeleteFile(path);
                    courseToUpdate.ImageUrl = await _fileSaver.SaveFileAsync(course.ImageFile);
                }
                _courseDal.Update(courseToUpdate);
            }
            else
            {
                throw new Exception("No course was found to update");
            }
            
        }

        public void UpdateCourse(Course course)
        {
            _courseDal.Update(course);
        }

        public void DeleteCourse(Guid courseId)
        {
            var course = _courseDal.Get(x => x.ID == courseId);
            _fileSaver.DeleteFile(course.ImageUrl);
            _courseDal.Delete(course);
        }

        public Course GetCourseById(Guid courseId)
        {
            return _courseDal.Get(x =>x.ID == courseId);
        }

        public List<Course> GetAllCourses()
        {
            return _courseDal.GetAll();
        }

        public List<Course> GetCoursesByInstructor(Guid instructorId)
        {
            return _courseDal.GetAll(x => x.ID == instructorId);
        }

        public List<Course> GetCoursesByUser(Guid userId)
        {
            return _courseDal.GetByUser(userId);
        }

        public void IncrementEnrollmentCout(Guid courseId)
        {
            var course = _courseDal.Get(c=>c.ID==courseId);
            if(course is not null)
            {
                course.EnrollmentCount++;
                UpdateCourse(course);
            }
        }

        public void DecrementEnrollmentCout(Guid courseId)
        {
            var course = _courseDal.Get(c => c.ID == courseId);
            if (course is not null)
            {
                course.EnrollmentCount--;
                UpdateCourse(course);
            }
        }

        public CourseWithAllDetails GetCourseWithDetail(Guid courseId)
        {
            return _courseDal.GetWithDetails(courseId);
        }

        public List<CourseWithAllDetails> GetAllCoursesWithDetail()
        {
            return _courseDal.GetAllWithDetails();
        }

        public List<CourseWithAllDetails> GetCoursesWithDetailsByUser(Guid userId)
        {
            return _courseDal.GetWithDetailsByUser(userId);
        }

        public List<CourseWithAllDetails> GetCoursesWithDetailsByInstructor(Guid instructorId)
        {
            return _courseDal.GetAllWithDetails().Where(c=>c.InstructorID==instructorId).ToList();
        }

        public List<CourseWithAllDetails> GetAllCoursesWithDetailByFilters(string? nameFilter, int? categoryFilter)
        {
            var courses = _courseDal.GetAllWithDetails();
            if (nameFilter != null)
                courses = courses.Where(c => c.Title.ToLower().Contains(nameFilter.ToLower())).ToList();
            if (categoryFilter != null)
                courses = courses.Where(c => (int)c.Category == categoryFilter).ToList();
            return courses;
        }
    }
}
