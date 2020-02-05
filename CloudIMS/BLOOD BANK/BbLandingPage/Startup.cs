using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBank.DonorManagement.RegisterDonor.Controllers;
using BloodBank.ResultManagement.ResultEntry.Controllers;
using CloudImsCommon.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApLandingPage
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
                ////Blood Bank Assemblies
                .AddApplicationPart(typeof(RegisterDonorController).Assembly)
                .AddApplicationPart(typeof(BloodBankResultEntryController).Assembly)

                //Add all controllers
                .AddControllersAsServices();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(options =>
                 {
                     options.Cookie.Name = "CloudCms";
                     options.LoginPath = "/debug/login";
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Home}/{folder=Home}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
