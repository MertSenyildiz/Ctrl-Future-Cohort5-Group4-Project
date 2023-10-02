using Microsoft.EntityFrameworkCore;
using Project.Core.DataAccess;
using Project.DataAccess.Abstract;
using Project.Models;

namespace Project.DataAccess.Concrete
{
    public class EfCourseDal:EntityRepositoryBase<Course, ProjectDbContext>, ICourseDal
    {
        IDbContextFactory<ProjectDbContext> _dbContextFactory;
        public EfCourseDal(IDbContextFactory<ProjectDbContext> dbContextFactory) : base(dbContextFactory)
        { }
    }
}
