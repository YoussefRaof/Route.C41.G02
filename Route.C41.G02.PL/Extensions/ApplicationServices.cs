using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Route.C4.G02.DAL.Data;
using Route.C4.G02.DAL.Models;
using Route.C41.G02.BLL;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.PL.Helpers;
using System;

namespace Route.C41.G02.PL.Extensions
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUniitOfWork, UnitOfWork>();
            services.AddAutoMapper(M => M.AddProfile(new MappingProfiles() ));

            //services.AddScoped<UserManager<ApplicationUser>>();
            //services.AddScoped<SignInManager<ApplicationUser>>();
            //services.AddScoped<RoleManager<ApplicationUser>>();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true; // #@%$
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 5;


                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

                options.User.RequireUniqueEmail = true;



            }).AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddAuthentication(); Called Automtically When calling AddIdentity & Add Authentication Used To Register 3 main Services Of Security Module
                


            return services;
        }
    }
}
