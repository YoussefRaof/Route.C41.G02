using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Route.C4.G02.DAL.Data;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.PL.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route.C41.G02.PL
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        //public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
            //Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to DI the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); //Register Built-In Services Required By MVC

            //services.AddTransient<ApplicationDbContext>();
            //services.AddScoped<ApplicationDbContext>();
            //services.AddSingleton<ApplicationDbContext>();
            //services.AddScoped<DbContextOptions<ApplicationDbContext>>();

            services.AddDbContext<ApplicationDbContext>(OptionsBuilder =>
            {
                OptionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            });

            services.AddApplicationServices();
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
