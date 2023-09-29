using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Project.Core.Security.Encryption;
using Project.Core.Security.JWT;
using System.Net;

namespace Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<IConfiguration>(provider => builder.Configuration);
            builder.Services.AddSingleton<ITokenHelper, JwtHelper>();

            var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer
                (
                opt => 
                {
                    opt.TokenValidationParameters.RoleClaimType = "role";
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer=tokenOptions.Issuer,
                        ValidAudience=tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey= SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
                        ClockSkew = TimeSpan.Zero
                    };
                    opt.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = ctx =>
                        {
                            if(ctx.Request.Cookies["X-Access-Token"] is not null)
                                ctx.Token = ctx.Request.Cookies["X-Access-Token"];
                            return Task.CompletedTask;
                        }
                    };
                }
                );

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            //unauthorized Page redirection
            //app.UseStatusCodePages(async ctx =>
            //{
            //    var response = ctx.HttpContext.Response;

            //    if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
            //            response.StatusCode == (int)HttpStatusCode.Forbidden)
            //        response.Redirect("/");
            //});

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}