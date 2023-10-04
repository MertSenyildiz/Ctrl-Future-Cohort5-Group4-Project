using Project.Core.DataAccess;
using Project.Models;

namespace Project.DataAccess.Abstract
{
    public interface ICourseDal: IEntityRepository<Course>
    {
        [Obsolete("GetByUser is deprecated, please use GetWithDetailsByUser instead.")]
        List<Course> GetByUser(Guid userId);
        List<CourseWithAllDetails> GetWithDetailsByUser(Guid userId);
        CourseWithAllDetails GetWithDetails(Guid CourseId);
        List<CourseWithAllDetails> GetAllWithDetails();
    }
}