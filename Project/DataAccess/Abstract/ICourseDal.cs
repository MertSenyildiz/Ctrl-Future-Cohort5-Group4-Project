using Project.Core.DataAccess;
using Project.Models;

namespace Project.DataAccess.Abstract
{
    public interface ICourseDal: IEntityRepository<Course>
    {
    }
}