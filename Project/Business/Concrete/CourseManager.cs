using Project.Business.Abstract;
using Project.Core.Helpers.File;
using Project.DataAccess.Abstract;
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

        public void UpdateCourse(Course course)
        {
            _courseDal.Update(course);
        }

        public void DeleteCourse(Guid courseId)
        {
            var course = _courseDal.Get(x => x.ID == courseId);
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
    }
}
