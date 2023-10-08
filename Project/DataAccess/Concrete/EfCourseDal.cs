using Microsoft.EntityFrameworkCore;
using Project.Core.DataAccess;
using Project.Data.Enum;
using Project.DataAccess.Abstract;
using Project.Migrations;
using Project.Models;

namespace Project.DataAccess.Concrete
{
    public class EfCourseDal:EntityRepositoryBase<Course, ProjectDbContext>, ICourseDal
    {
        public EfCourseDal(IDbContextFactory<ProjectDbContext> dbContextFactory) : base(dbContextFactory)
        { }

        public List<CourseWithAllDetails> GetAllWithDetails()
        {
            using var context = _dbContextFactory.CreateDbContext();
            var courses = (from c in context.Courses
                          join u in context.Users
                          on c.InstructorID equals u.ID
                          select new CourseWithAllDetails
                          {
                              ID = c.ID,
                              Title = c.Title,
                              Description = c.Description,
                              InstructorID = c.InstructorID,
                              Category = c.Category,
                              EnrollmentCount = c.EnrollmentCount,
                              ImageUrl = c.ImageUrl,
                              Email = u.Email,
                              Username = u.Username,
                          }).ToList();
            return courses;
        }

        public List<Course> GetByUser(Guid userId)
        {
            using var context=_dbContextFactory.CreateDbContext();
            var courses = (from c in context.Courses
                          join e in context.Enrollments
                             on c.ID equals e.CourseID
                          where e.UserID == userId
                          select c).ToList();
            return courses;
        }

        public CourseWithAllDetails GetWithDetails(Guid CourseId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var course = (from c in context.Courses
                          where c.ID == CourseId
                          join u in context.Users
                          on c.InstructorID equals u.ID
                          select new CourseWithAllDetails
                          {
                            ID=c.ID,
                            Title=c.Title,
                            Description=c.Description,
                            InstructorID=c.InstructorID,
                            Category = c.Category, 
                            EnrollmentCount=c.EnrollmentCount,
                            ImageUrl = c.ImageUrl,
                            Email=u.Email,
                            Username=u.Username,
                          }).FirstOrDefault();
            return course;
        }

        public List<CourseWithAllDetails> GetWithDetailsByUser(Guid userId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var courses = (from c in context.Courses
                           join e in context.Enrollments
                           on c.ID equals e.CourseID
                           join u in context.Users
                           on c.InstructorID equals u.ID
                           where e.UserID == userId
                           select new CourseWithAllDetails
                           {
                               ID = c.ID,
                               Title = c.Title,
                               Description = c.Description,
                               InstructorID = c.InstructorID,
                               Category = c.Category,
                               EnrollmentCount = c.EnrollmentCount,
                               ImageUrl = c.ImageUrl,
                               Email = u.Email,
                               Username = u.Username,
                           }).ToList();
            return courses;
        }
    }
}
