using Client.BLL.Interfaces;
using Client.BLL.Repositories;
using Client.DAL.Data;
using Client.PL.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.PL
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); //  Register built-in Services Required By MVC

            ///services.AddScoped<ApplicationDbContext>();
            ///services.AddScoped<DbContextOptions<ApplicationDbContext>>();
 
            ///services.AddDbContext<ApplicationDbContext>
            ///    (
            ///    contextLifetime: ServiceLifetime.Scoped,
            ///    optionsLifetime: ServiceLifetime.Scoped
            ///    );
            

            services.AddDbContext<ApplicationDbContext>(
                options =>options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                                                       );

            //ApplicationServicesExtensions.AddApplicationServices(services);
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
