using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Mvc.Razor;
using CloudImsCommon.Database;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using CloudImsCommon.Extensions;
using Microsoft.AspNetCore.Mvc.Authorization;
using BloodBank.DonorManagement.RegisterDonor.Controllers;
using BloodBank.ResultManagement.ResultEntry.Controllers;

using BloodBank.Controllers;

namespace CloudCms
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
            services.AddSingleton<AppTenantManager>();

            
            services.AddMvc()

                //AP Assemblies
                //.AddApplicationPart(typeof(ApLandingPageController).Assembly)
                //.AddApplicationPart(typeof(BlockAndSlideController).Assembly)
                //.AddApplicationPart(typeof(ApSampleReceptionController).Assembly)
                //.AddApplicationPart(typeof(ApResultEntryController).Assembly)
                //.AddApplicationPart(typeof(PapSmearResultEntryController).Assembly)

                //Blood Bank Assemblies
                .AddApplicationPart(typeof(BbLandingPageController).Assembly)
                .AddApplicationPart(typeof(RegisterDonorController).Assembly)
                .AddApplicationPart(typeof(BloodBankResultEntryController).Assembly)

                //Add all controllers
                .AddControllersAsServices();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(options =>
                 {
                     options.Cookie.Name = "CloudCms";
                     options.LoginPath = "/Home/Home/Home/Login";
                     options.ExpireTimeSpan = TimeSpan.FromMinutes(-1);
                     options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                 });
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

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //      name: "default",
            //      template: "{area=Home}/{controller=Home}/{action=Index}/{id?}"
            //    );
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Home}/{folder=Home}/{controller=main}/{action=landing-page}/{id?}");
            });

        }
    }
}
