using Microsoft.EntityFrameworkCore;
using Project.Business.Abstract;
using Project.Business.Concrete;
using Project.Core.Helpers.File;
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
                services.AddSingleton<IUserDal, EfUserDal>();
                services.AddSingleton<ITokenHelper, JwtHelper>();
                services.AddSingleton<IUserService, UserManager>();
                services.AddSingleton<IAuthService, AuthManager>();
            }
            public static void InjectDbContextFactory(this IServiceCollection services, IConfiguration configuration)
            {
                services.AddDbContextFactory<ProjectDbContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString("ProjectConnectionString"))
                );
            }

            public static void InjectConfigurableServices(this IServiceCollection services, IConfiguration configuration)
            {
                FileSaver fileSaver = new FileSaver(configuration.GetSection("FileFolder").Get<string>());
                services.AddSingleton<IFileHelper>(fileSaver);
            }
    }
}
