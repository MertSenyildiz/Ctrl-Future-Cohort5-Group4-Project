using Microsoft.EntityFrameworkCore;
using Project.Core.DataAccess;
using Project.DataAccess.Abstract;
using Project.Models;

namespace Project.DataAccess.Concrete
{
    public class EfUserDal : EntityRepositoryBase<User,ProjectDbContext>, IUserDal
    {
        IDbContextFactory<ProjectDbContext> _dbContextFactory;
        public EfUserDal(IDbContextFactory<ProjectDbContext> dbContextFactory) :base(dbContextFactory)
        {}
    }
}
