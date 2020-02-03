using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CloudCmsCommon.Database
{
    public class AppDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var projectDirectory = Directory.GetCurrentDirectory();
            var executableDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(executableDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("CcmsConnection");
            connectionString = connectionString.Replace("<COMPANY_ID>", "tgsc");

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseMySQL(connectionString);
            return new AppDbContext(builder.Options);
        }
    }
}
