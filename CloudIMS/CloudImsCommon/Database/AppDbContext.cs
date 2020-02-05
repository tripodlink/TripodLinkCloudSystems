using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CloudImsCommon.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public static AppDbContext CreateInstance(String companyId)
        {

            var projectDirectory = Directory.GetCurrentDirectory();
            var executableDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(executableDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("CcmsConnection");
            connectionString = connectionString.Replace("<COMPANY_ID>", companyId.ToLower());

            DbContextOptionsBuilder<AppDbContext> builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseMySQL(connectionString);

            var dbcontext = new AppDbContext(builder.Options);

            return dbcontext;
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
        
        public DbSet<Company> Company { get; set; }
        public DbSet<EventTable> EventTable { get; set; }


        //Dictionary tables
        public DbSet<Clinician> Clinicians { get; set; }
        public DbSet<Item> OrderItems { get; set; }
        public DbSet<UnitCode> UnitCodes { get; set; }

        //User Account tables
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserAccountGroup> UserAccountGroups { get; set; }

        //Program Menu tables
        public DbSet<ProgramRoot> ProgramRoots { get; set; }
        public DbSet<ProgramFolder> ProgramFolders { get; set; }
        public DbSet<ProgramMenu> ProgramMenus { get; set; }

        //User Group tables
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserGroupProgram> UserGroupPrograms { get; set; }
    }
}
