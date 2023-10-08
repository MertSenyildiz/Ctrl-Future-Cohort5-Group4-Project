using Microsoft.EntityFrameworkCore;
using Project.Core.DataAccess;
using Project.DataAccess.Abstract;
using Project.Models;

namespace Project.DataAccess.Concrete
{
    public class EfEnrollmentDal : EntityRepositoryBase<Enrollment, ProjectDbContext>, IEnrollmentDal
    {
        public EfEnrollmentDal(IDbContextFactory<ProjectDbContext> dbContextFactory) : base(dbContextFactory)
        { }

        public List<EnrollmentWithAllDetails> GetAllWithDetails()
        {
            using var context=_dbContextFactory.CreateDbContext();
            var enrollments=(from e in context.Enrollments
                            join u in context.Users
                            on e.UserID equals u.ID
                            select new EnrollmentWithAllDetails
                            {
                                ID = e.ID,
                                EnrollmentDate = e.EnrollmentDate,
                                CourseID= e.CourseID,
                                UserID= e.UserID,

                                Email=u.Email,
                                Username=u.Username,

                            }).ToList();
            return enrollments;
        }
    }
}
