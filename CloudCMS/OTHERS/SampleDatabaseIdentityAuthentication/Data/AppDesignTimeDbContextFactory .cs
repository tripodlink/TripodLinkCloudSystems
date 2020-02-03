using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDatabaseIdentityAuthentication.Data
{
    public class AppDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("CcmsConnection");
            connectionString = connectionString.Replace("<DATABASE_NAME>", "xcms_tgsc");

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseMySQL(connectionString);
            return new AppDbContext(builder.Options);
        }
    }
}
