using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CloudImsCommon.Debug.Controllers;
using CloudImsCommon.Database;
using Microsoft.EntityFrameworkCore;

namespace ApResultEntry
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add AppDbContext
            services.AddDbContextPool<AppDbContext>(
                options => options.UseMySQL(AppDbContext.GetConnectionString()));


            //Add AutoLoginController Assembly -> CloudImsCommon
            services.AddMvc()
                .AddApplicationPart(typeof(AutoLoginController).Assembly)
                .AddControllersAsServices();

            //Add cookie
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(options =>
                 {
                     options.Cookie.Name = "CloudIms";
                     options.LoginPath = "/Debug/Login";
                     options.ExpireTimeSpan = TimeSpan.FromMinutes(-1);
                     options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                 });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext dbContext)
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

            //ensure that database is in-sync with models
            dbContext.Database.Migrate();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Home}/{folder=Home}/{controller=main}/{action=landing-page}/{id?}");
            });
        
        }
    }
}
