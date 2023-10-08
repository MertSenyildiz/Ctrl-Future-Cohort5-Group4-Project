using Microsoft.EntityFrameworkCore;
using Project.Core.DataAccess;
using Project.DataAccess.Abstract;
using Project.Models;

namespace Project.DataAccess.Concrete
{
    public class EfAssignmentDal : EntityRepositoryBase<Assignment, ProjectDbContext>, IAssignmentDal
    {
        public EfAssignmentDal(IDbContextFactory<ProjectDbContext> dbContextFactory) : base(dbContextFactory)
        { }

        public List<AssignmentWithAllDetails> GetAllWithDetails()
        {
            var context = _dbContextFactory.CreateDbContext();
            var assignments=(from a in context.Assignments
                             join c in context.Courses
                             on a.CourseID equals c.ID
                             select new AssignmentWithAllDetails
                             {
                                 ID=a.ID,
                                 Description=a.Description,
                                 DueDate=a.DueDate,
                                 Title=a.Title,
                                 CourseID=a.CourseID,
                                 CourseTitle=c.Title,
                                 CourseDescription=c.Description,
                             }
                ).OrderByDescending(a=>a.DueDate).AsNoTracking().ToList();
            return assignments;
        }

        public AssignmentWithAllDetails GetWithDetails(Guid assignmentId)
        {
            var context = _dbContextFactory.CreateDbContext();
            var assignment = (from a in context.Assignments
                               where a.ID==assignmentId
                               join c in context.Courses
                               on a.CourseID equals c.ID
                               select new AssignmentWithAllDetails
                               {
                                   ID = a.ID,
                                   Description = a.Description,
                                   DueDate = a.DueDate,
                                   Title = a.Title,
                                   CourseID = a.CourseID,
                                   CourseTitle = c.Title,
                                   CourseDescription = c.Description,
                               }
                ).AsNoTracking().FirstOrDefault();
            return assignment;
        }

        public List<AssignmentWithAllDetails> GetWithDetailsByCourse(Guid courseId)
        {
            var context = _dbContextFactory.CreateDbContext();
            var assignments = (from a in context.Assignments
                              where a.CourseID ==courseId
                              join c in context.Courses
                              on a.CourseID equals c.ID
                              select new AssignmentWithAllDetails
                              {
                                  ID = a.ID,
                                  Description = a.Description,
                                  DueDate = a.DueDate,
                                  Title = a.Title,
                                  CourseID = a.CourseID,
                                  CourseTitle = c.Title,
                                  CourseDescription = c.Description,
                              }
                ).OrderBy(a=>a.DueDate).AsNoTracking().ToList();
            return assignments;
        }
    }
}
