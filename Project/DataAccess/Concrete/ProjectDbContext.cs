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
    }
}
