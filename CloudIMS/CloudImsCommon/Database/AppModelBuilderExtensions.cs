using Microsoft.EntityFrameworkCore;
using CloudImsCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CloudImsCommon.Database
{
    public static class AppModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            SeedCompany(modelBuilder);
            SeedEventTable(modelBuilder);


            //User Accounts
            SeedUserAccounts(modelBuilder);
            modelBuilder.Entity<UserAccountGroup>().HasKey(c => new { c.UserAccountID, c.UserGroupID });
            SeedUserAccountGroupForUser_SYSAD(modelBuilder);


            //Inventory Management Programs
            SeedInventoryManagementProgramMenus(modelBuilder);
            SeedDataDictionaryProgramMenus(modelBuilder);
            SeedUserManagementProgramMenus(modelBuilder);

            //Dictionary Seeds
            SeedUnitCodes(modelBuilder);
            SeedManufacturer(modelBuilder);
            SeedSupplier(modelBuilder);
            SeedItemGroup(modelBuilder);
            SeedDepartment(modelBuilder);
            SeedItemMaster(modelBuilder);

            SeedAutoNumber(modelBuilder);
            //Dashboard seeds
            SeedDashboardProgramMenus(modelBuilder);

            //ReportManagement
            SeedReportManagementProgramMenus(modelBuilder);

            //User Group Seeds
            modelBuilder.Entity<UserGroupProgram>().HasKey(c => new {c.UserGroupID, c.ProgramMenuID });

            modelBuilder.Entity<ItemMasterUnit>().HasKey(itm => new { itm.ID, itm.itemMasterUnitUnit });

            modelBuilder.Entity<InventoryOutTrxDetail>().HasKey(d => new { d.TransactionNo, d.ItemID, d.In_TrxNo});

            CreateSystemAdminGroupWithUserPrograms(modelBuilder);

          

        }
        private static void SeedAutoNumber(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AutoNumber>().HasData(
               new AutoNumber {ID = "INVIN",Text_Prefix="TI", Date_Prefix="YY",Auto_Length="8",Last_Value="1",Current_year="2020"},
               new AutoNumber { ID = "INVOUT", Text_Prefix = "TO", Date_Prefix = "YY", Auto_Length = "8", Last_Value = "1", Current_year = "2020" }
               );
        }

        private static void SeedCompany(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasData(
               new Company { ID = "000", CompanyID = "NEW", CompanyName = "NEW CLOUD IMS CUSTOMER" }
               );
        }
        
        private static void SeedEventTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventTable>().HasData(
                new EventTable { Code = "IV_IN", Description = "Inventory In" }
                );
        }

        private static void SeedItemMaster(ModelBuilder modelBuilder)
        {
            //create unique index 
            modelBuilder.Entity<ItemMaster>().HasData(
                new ItemMaster { ID = "01", ItemGroup = "100",ItemName = "Default Item",Unit ="102", Supplier="100", Manufacturer="100"}
               );
            modelBuilder.Entity<ItemMasterUnit>().HasData(
                new ItemMasterUnit { ID ="01",itemMasterUnitUnit="102",itemMasterUnitConversion="1"}
                );
        }


        private static void SeedUnitCodes(ModelBuilder modelBuilder)
        {
            //create unique index 
            modelBuilder.Entity<UnitCode>().HasIndex(idx => new { idx.Description }).IsUnique(false).HasName("IDX_DESCRIPTION");
            modelBuilder.Entity<UnitCode>().HasData(
                new UnitCode { Code = "100", Description = "Box" },
                new UnitCode { Code = "101", Description = "Tray" },
                new UnitCode { Code = "102", Description = "Pieces" }
               );
        }


        private static void SeedManufacturer(ModelBuilder modelBuilder)
        {
            //create unique index 
            modelBuilder.Entity<Manufacturer>().HasIndex(idx => new { idx.ManufactName }).IsUnique(false).HasName("IDX_DESCRIPTION");
            modelBuilder.Entity<Manufacturer>().HasData(
                new Manufacturer { ID = "100", ManufactName = "Default Manufacturer" }
               );
        }
        private static void SeedSupplier(ModelBuilder modelBuilder)
        {
            //create unique index 
            modelBuilder.Entity<Supplier>().HasIndex(idx => new { idx.Name }).IsUnique(false).HasName("IDX_DESCRIPTION");
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { ID = "100", Name = "Default Supplier" }
               );
        }
        private static void SeedItemGroup(ModelBuilder modelBuilder)
        {
            //create unique index 
            modelBuilder.Entity<ItemGroup>().HasIndex(idx => new { idx.ItemGroupName }).IsUnique(false).HasName("IDX_DESCRIPTION");
            modelBuilder.Entity<ItemGroup>().HasData(
                new ItemGroup { ID = "100", ItemGroupName = "Default ItemGroup" }
               );
        }
          private static void SeedDepartment(ModelBuilder modelBuilder)
        {
            //create unique index 
            modelBuilder.Entity<Department>().HasIndex(idx => new { idx.DepartmentName }).IsUnique(false).HasName("IDX_DESCRIPTION");
            modelBuilder.Entity<Department>().HasData(
                new Department { ID = "100", DepartmentName = "Default DepartmentName" }
               );
        }


        private static void SeedUserAccounts(ModelBuilder modelBuilder)
        {
            //create unique index 
            modelBuilder.Entity<UserAccount>().HasData(
               new UserAccount { UserID = "SYSAD", UserName = "SYSTEM ADMINISTRATOR", Password=".00000", IsActive = true , CreatedOn = DateTime.Now , CreatedBy = "SYSTEM", UpdatedOn = DateTime.Now, UpdatedBy = "SYSTEM"  }

               );
        }

        private static void SeedUserAccountGroupForUser_SYSAD(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccountGroup>().HasData(
                new UserAccountGroup { UserAccountID = "SYSAD", UserGroupID = "ADMIN" }
                );
        }

        private static void SeedReportManagementProgramMenus(ModelBuilder modelBuilder)
        {
            //create Sample Management folder 
            modelBuilder.Entity<ProgramFolder>().HasData(
               new ProgramFolder { ID = "RPT", Name = "Report Management",Icon= "fa fa-file-text", RouteAttribute = "report-management", SequenceNo = 10 }
               );

            //create Sample Management menu 
            modelBuilder.Entity<ProgramMenu>().HasData(
               new ProgramMenu { ID = "RPT_IVM_IN", Name = "(Report) Inventory In", ProgramFolderID = "RPT", IconName = "fa fa-desktop", RouteAttribute = "report-inventory-in", SequenceNo = 10 },
               new ProgramMenu { ID = "RPT_IVM_OUT", Name = "(Report) Inventory Out", ProgramFolderID = "RPT", IconName = "fa fa-desktop", RouteAttribute = "report-inventory-out", SequenceNo = 20 }
               );
        }


        private static void SeedDashboardProgramMenus(ModelBuilder modelBuilder)
        {
            //create Sample Management folder 
            modelBuilder.Entity<ProgramFolder>().HasData(
               new ProgramFolder { ID = "DASH", Name = "Dashboard", Icon = "fa fa-tachometer", RouteAttribute = "dashboard", SequenceNo = 10 }
               );

            modelBuilder.Entity<ProgramMenu>().HasData(
              new ProgramMenu { ID = "DASH", Name = "Dashboard", ProgramFolderID = "DASH", IconName = "fa fa-tachometer", RouteAttribute = "dashboard", SequenceNo = 10 }
              );
        }

        private static void SeedInventoryManagementProgramMenus(ModelBuilder modelBuilder)
        {
            //create Sample Management folder 
            modelBuilder.Entity<ProgramFolder>().HasData(
               new ProgramFolder { ID = "IVM", Name = "Inventory Management",Icon = "fa fa-truck", RouteAttribute = "inventory-management", SequenceNo = 10 }
               );

            //create Sample Management menu 
            modelBuilder.Entity<ProgramMenu>().HasData(
               new ProgramMenu { ID = "IVM_IN", Name = "Inventory In", ProgramFolderID = "IVM", IconName = "fa fa-desktop", RouteAttribute = "inventory-in", SequenceNo = 10 },
               new ProgramMenu { ID = "IVM_OUT", Name = "Inventory Out", ProgramFolderID = "IVM", IconName = "fa fa-desktop", RouteAttribute = "inventory-out", SequenceNo = 20 }
               );
        }

        private static void SeedDataDictionaryProgramMenus(ModelBuilder modelBuilder)
        {
            //create Sample Management folder 
            modelBuilder.Entity<ProgramFolder>().HasData(
               new ProgramFolder { ID = "DIC", Name = "Data Dictionary", Icon = "fa fa-file-code-o", RouteAttribute = "data-dictionary", SequenceNo = 20 }
               );

            //create Sample Management menu 
            modelBuilder.Entity<ProgramMenu>().HasData(
               new ProgramMenu { ID = "DIC_UNIT", Name = "Unit", ProgramFolderID = "DIC", IconName = "fa fa-desktop", RouteAttribute = "unit-code", SequenceNo = 60 },
               new ProgramMenu { ID = "DIC_SUP", Name = "Supplier", ProgramFolderID = "DIC", IconName = "fa fa-desktop", RouteAttribute = "supplier", SequenceNo = 50 },
               new ProgramMenu { ID = "DIC_MANU", Name = "Manufacturer", ProgramFolderID = "DIC", IconName = "fa fa-desktop", RouteAttribute = "manufacturer", SequenceNo = 40 },
               new ProgramMenu { ID = "DIC_ITEM", Name = "Item Master", ProgramFolderID = "DIC", IconName = "fa fa-desktop", RouteAttribute = "item-master", SequenceNo = 30 },
               new ProgramMenu { ID = "DIC_ITEMGRP", Name = "Item Group", ProgramFolderID = "DIC", IconName = "fa fa-desktop", RouteAttribute = "item-group", SequenceNo = 20 },
               new ProgramMenu { ID = "DIC_DEP", Name = "Department", ProgramFolderID = "DIC", IconName = "fa fa-desktop", RouteAttribute = "department", SequenceNo = 10 });
        }

        private static void SeedUserManagementProgramMenus(ModelBuilder modelBuilder)
        {
            //create Sample Management folder 
            modelBuilder.Entity<ProgramFolder>().HasData(
               new ProgramFolder { ID = "UM", Name = "User Management", Icon = "fa fa-user-circle-o", RouteAttribute = "user-management", SequenceNo = 30 }
               );

            //create Sample Management menu 
            modelBuilder.Entity<ProgramMenu>().HasData(
               new ProgramMenu { ID = "UM_USERACC", Name = "User Account", IconName= "fa fa-desktop", ProgramFolderID = "UM", RouteAttribute = "user-account", SequenceNo = 10 },
               new ProgramMenu { ID = "UM_USERGRP", Name = "User Group", IconName = "fa fa-desktop", ProgramFolderID = "UM", RouteAttribute = "user-group", SequenceNo = 20 },
               new ProgramMenu { ID = "UM_USERPROF", Name = "User Profile", IconName = "fa fa-desktop", ProgramFolderID = "UM", RouteAttribute = "user-profile", SequenceNo = 30 });
        }

        private static void CreateSystemAdminGroupWithUserPrograms(ModelBuilder modelBuilder)
        {
            //create Result Management folder 
            modelBuilder.Entity<UserGroup>().HasData(
               new UserGroup { ID = "ADMIN", Name = "SYSTEM ADMINISTRATORS",IsApprover = true}
               );

            //create Result Management menu 
            modelBuilder.Entity<UserGroupProgram>().HasData(
               //Report Management
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "RPT_IVM_IN" },
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "RPT_IVM_OUT" },

               //Inventory Management
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "IVM_IN" },
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "IVM_OUT" },

               //Data Dictionary
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "DIC_UNIT" },
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "DIC_SUP" },
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "DIC_MANU" },
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "DIC_ITEM" },
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "DIC_ITEMGRP" },
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "DIC_DEP" },

               //DASHBOARD
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "DASH" },

               //User Management
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "UM_USERACC" },
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "UM_USERGRP" },
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "UM_USERPROF" }
               );
        }
    }
}
