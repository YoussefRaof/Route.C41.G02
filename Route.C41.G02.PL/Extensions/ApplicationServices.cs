using Microsoft.Extensions.DependencyInjection;
using Route.C41.G02.BLL;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.PL.Helpers;

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

            return services;
        }
    }
}
