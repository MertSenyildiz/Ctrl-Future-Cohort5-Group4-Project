using Microsoft.EntityFrameworkCore;
using Project.Models;
using System.Data;

namespace Project.DataAccess.Concrete
{
    public class ProjectDbContext:DbContext
    {
        public ProjectDbContext(DbContextOptions options):base(options)
        {}
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }
}
