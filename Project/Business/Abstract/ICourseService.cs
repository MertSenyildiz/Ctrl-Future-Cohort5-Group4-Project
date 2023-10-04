﻿using Project.Models;

namespace Project.Business.Abstract
{
    public interface ICourseService
    {

        [Obsolete("GetCourseById is deprecated, please use GetCourseWithDetail instead.")]
        Course GetCourseById(Guid courseId);
        CourseWithAllDetails GetCourseWithDetail(Guid courseId);



        [Obsolete("GetAllCourses is deprecated, please use GetAllCoursesWithDetail instead.")]
        List<Course> GetAllCourses();
        List<CourseWithAllDetails> GetAllCoursesWithDetail();



        [Obsolete("GetCoursesByInstructor is deprecated, please use GetCoursesWithDetailsByInstructor instead.")]
        List<Course> GetCoursesByInstructor(Guid instructorId);
        List<CourseWithAllDetails> GetCoursesWithDetailsByInstructor(Guid instructorId);



        [Obsolete("GetCoursesByUser is deprecated, please use GetCoursesWithDetailsByUser instead.")]
        List<Course> GetCoursesByUser(Guid userId);
        List<CourseWithAllDetails> GetCoursesWithDetailsByUser(Guid userId);


        void IncrementEnrollmentCout(Guid courseId);
        void DecrementEnrollmentCout(Guid courseId);
        Task CreateCourseAsync(CourseToAddDto course);
        void UpdateCourse(Course course);
        void DeleteCourse(Guid courseId);
    }
}
