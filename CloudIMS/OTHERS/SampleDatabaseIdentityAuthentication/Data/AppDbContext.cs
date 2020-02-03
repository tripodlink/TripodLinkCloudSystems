using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SampleDatabaseIdentityAuthentication.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace SampleDatabaseIdentityAuthentication.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
        }

        public static AppDbContext CreateInstance(String companyId)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("CcmsConnection");
            connectionString = connectionString.Replace("<COMPANY_ID>", companyId.ToLower());

            DbContextOptionsBuilder<AppDbContext> builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseMySQL(connectionString);

            var dbcontext = new AppDbContext(builder.Options);         

            return dbcontext;
        }

        public DbSet<UserAccount> UserAccounts { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}
