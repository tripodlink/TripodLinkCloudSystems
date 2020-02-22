using AnatomicalPathology.Controllers;
using AnatomicalPathology.ResultManagement.ApResultEntry.Controllers;
using AnatomicalPathology.ResultManagement.PapSmearResultEntry.Controllers;
using AnatomicalPathology.SampleManagement.ApSampleReception.Controllers;
using AnatomicalPathology.SampleManagement.BlockAndSlide.Controllers;
using BloodBank.Controllers;
using BloodBank.DonorManagement.RegisterDonor.Controllers;
using BloodBank.ResultManagement.ResultEntry.Controllers;
using CloudImsCommon.Database;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CloudIms
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

            services.AddDbContextPool<AppDbContext>(
                options => options.UseMySQL(AppDbContext.GetConnectionString()));


            services.AddMvc()

                //AP Assemblies
                .AddApplicationPart(typeof(ApLandingPageController).Assembly)
                .AddApplicationPart(typeof(BlockAndSlideController).Assembly)
                .AddApplicationPart(typeof(ApSampleReceptionController).Assembly)
                .AddApplicationPart(typeof(ApResultEntryController).Assembly)
                .AddApplicationPart(typeof(PapSmearResultEntryController).Assembly)

                //Blood Bank Assemblies
                .AddApplicationPart(typeof(BbLandingPageController).Assembly)
                .AddApplicationPart(typeof(RegisterDonorController).Assembly)
                .AddApplicationPart(typeof(BloodBankResultEntryController).Assembly)

                //Add all controllers
                .AddControllersAsServices();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(options =>
                 {
                     options.Cookie.Name = "CloudIms";
                     options.LoginPath = "/Home/Home/Home/Login";
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
