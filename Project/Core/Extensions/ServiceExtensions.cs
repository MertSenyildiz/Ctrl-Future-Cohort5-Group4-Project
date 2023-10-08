using Microsoft.EntityFrameworkCore;
using Project.Business.Abstract;
using Project.Business.Concrete;
using Project.Core.Helpers.FileHelpers;
using Project.Core.Security.JWT;
using Project.DataAccess.Abstract;
using Project.DataAccess.Concrete;

namespace Project.Core.Extensions
{
    public static class ServiceExtensions
    {
            public static void InjectServices(this IServiceCollection services)
            {
                services.AddSingleton<ITokenHelper, JwtHelper>();

                services.AddSingleton<IUserDal, EfUserDal>();
                services.AddSingleton<ICourseDal, EfCourseDal>();
                services.AddSingleton<IEnrollmentDal, EfEnrollmentDal>();
                services.AddSingleton<IAssignmentDal, EfAssignmentDal>();

                services.AddSingleton<IUserService, UserManager>();
                services.AddSingleton<IAuthService, AuthManager>();
                services.AddSingleton<IEnrollmentService, EnrollmentManager>();

                services.AddSingleton<ICourseService, CourseManager>();
                services.AddSingleton<IAssignmentService, AssignmentManager>();

                services.AddSingleton<IAdminService, AdministrationManager>();

                services.AddSingleton<IInstructorService, InstructorManager>();
            }

            public static void InjectDbContextFactory(this IServiceCollection services, IConfiguration configuration)
            {
            var isDevelopment = string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "development", StringComparison.InvariantCultureIgnoreCase);
            if(isDevelopment)
            {
                Console.Error.WriteLine("development");
                services.AddDbContextFactory<ProjectDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("ProjectConnectionString"))
            );
            }
            else
            {
                Console.Error.WriteLine("not in development");
                services.AddDbContextFactory<ProjectDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("AzureConnectionString"))
            );
            }
            
        }

            public static void InjectConfigurableServices(this IServiceCollection services, IConfiguration configuration)
            {
                FileSaver fileSaver = new FileSaver(configuration.GetSection("FileFolder").Get<string>());
                services.AddSingleton<IFileHelper>(fileSaver);
            }
    }
}
