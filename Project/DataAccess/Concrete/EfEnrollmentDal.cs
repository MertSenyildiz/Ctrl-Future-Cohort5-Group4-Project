using Microsoft.EntityFrameworkCore;
using Project.Core.DataAccess;
using Project.DataAccess.Abstract;
using Project.Models;

namespace Project.DataAccess.Concrete
{
    public class EfEnrollmentDal : EntityRepositoryBase<Enrollment, ProjectDbContext>, IEnrollmentDal
    {
        IDbContextFactory<ProjectDbContext> _dbContextFactory;
        public EfEnrollmentDal(IDbContextFactory<ProjectDbContext> dbContextFactory) : base(dbContextFactory)
        { }
    }
}
