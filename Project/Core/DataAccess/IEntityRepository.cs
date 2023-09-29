using System.Linq.Expressions;
using System.Security.Principal;

namespace Project.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, new()
    {
        public T Get(Expression<Func<T, bool>> filter);
        public List<T> GetAll(Expression<Func<T, bool>> filter = null);
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
