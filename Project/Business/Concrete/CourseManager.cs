using Project.Business.Abstract;
using Project.DataAccess.Abstract;
using Project.Models;
using System;
using System.Collections.Generic;


namespace Project.Business.Concrete
{
    public class CourseManagement : ICourseService
    {
        
        private readonly ICourseDal _courseDal;

        public CourseManagement(ICourseDal courseDal)
        {
            _courseDal = courseDal;
        }

        public void CreateCourse(Course course)
        {
            _courseDal.Add(course);
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
