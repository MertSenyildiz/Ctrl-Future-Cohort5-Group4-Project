using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Project.Core.DataAccess
{
    public class EntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TContext : DbContext
        where TEntity : class,new()
    {
        protected IDbContextFactory<TContext> _dbContextFactory;
        public EntityRepositoryBase(IDbContextFactory<TContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public void Add(TEntity entity)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var entityAdded=context.Entry(entity);
            entityAdded.State = EntityState.Added;
            context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var entityDeleted = context.Entry(entity);
            entityDeleted.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return context.Set<TEntity>().FirstOrDefault(filter);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return filter == null ? context.Set<TEntity>().AsNoTracking().ToList() : context.Set<TEntity>().AsNoTracking().Where(filter).ToList();
        }

        public void Update(TEntity entity)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var entityUpdated = context.Entry(entity);
            entityUpdated.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
