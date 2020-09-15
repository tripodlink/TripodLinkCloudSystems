using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        public static AppDbContext CreateAppDbContext() {
            DbContextOptionsBuilder<AppDbContext> builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseMySQL(GetConnectionString());

            return new AppDbContext(builder.Options);
        }

        public static String GetConnectionString() {
            var projectDirectory = Directory.GetCurrentDirectory();
            var executableDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(executableDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString =  configuration.GetConnectionString("ImsConnection");

            return connectionString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {          
            base.OnModelCreating(modelBuilder);            
            modelBuilder.Seed();
        }

        public DbSet<Company> Company { get; set; }
        public DbSet<EventTable> EventTable { get; set; }


        //Dictionary tables
        public DbSet<UnitCode> UnitCodes { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<ItemMaster> ItemMasters { get; set; }
        public DbSet<ItemGroup> ItemGroups { get; set; }
        public DbSet<ItemMasterUnit> itemMasterUnits { get; set; }

        //User Account tables
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserAccountGroup> UserAccountGroups { get; set; }

        //Program Menu tables
        public DbSet<ProgramFolder> ProgramFolders { get; set; }
        public DbSet<ProgramMenu> ProgramMenus { get; set; }

        //User Group tables
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserGroupProgram> UserGroupPrograms { get; set; }

        public DbSet<InventoryInTrxHeader> InventoryInTrxHeaders { get; set; }
        public DbSet<InventoryInTrxDetail> InventoryInTrxDetails { get; set; }
        public DbSet<InventoryOutTrxHeader> InventoryOutTrxHeaders { get; set; }
        public DbSet<InventoryOutTrxDetail> InventoryOutTrxDetails { get; set; }
        public DbSet<DefectedItemsModel> DefectedItemsModel { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<AutoNumber> autoNumbers { get; set; }
        public DbSet<ItemTracking> itemTrackings { get; set; }
    }
}
