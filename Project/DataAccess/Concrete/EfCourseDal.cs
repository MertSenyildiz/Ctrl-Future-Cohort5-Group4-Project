using Microsoft.EntityFrameworkCore;
using Project.Core.DataAccess;
using Project.DataAccess.Abstract;
using Project.Models;

namespace Project.DataAccess.Concrete
{
    public class EfCourseDal:EntityRepositoryBase<Course, ProjectDbContext>, ICourseDal
    {
        public EfCourseDal(IDbContextFactory<ProjectDbContext> dbContextFactory) : base(dbContextFactory)
        { }

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
    }
}
