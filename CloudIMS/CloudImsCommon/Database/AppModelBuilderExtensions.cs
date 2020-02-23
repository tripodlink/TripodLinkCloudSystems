using Microsoft.EntityFrameworkCore;
using CloudImsCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            //User Group Seeds
            modelBuilder.Entity<UserGroupProgram>().HasKey(c => new {c.UserGroupID, c.ProgramMenuID });
            CreateSystemAdminGroupWithUserPrograms(modelBuilder);

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

       
        private static void SeedUnitCodes(ModelBuilder modelBuilder)
        {
            //create unique index 
            modelBuilder.Entity<UnitCode>().HasIndex(idx => new { idx.Description }).IsUnique(false).HasName("IDX_DESCRIPTION");
            modelBuilder.Entity<UnitCode>().HasData(
                new UnitCode { Code = "100", Description = "Meter" },
                new UnitCode { Code = "101", Description = "Kilo" },
                new UnitCode { Code = "102", Description = "Gram" }
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

        private static void SeedInventoryManagementProgramMenus(ModelBuilder modelBuilder)
        {
            //create Sample Management folder 
            modelBuilder.Entity<ProgramFolder>().HasData(
               new ProgramFolder { ID = "DASH", Name = "Dashboard", RouteAttribute = "dashboard", SequenceNo = 0 },
               new ProgramFolder { ID = "IVM", Name = "Inventory Management", RouteAttribute = "inventory-management", SequenceNo = 10 }
               );

            //create Sample Management menu 
            modelBuilder.Entity<ProgramMenu>().HasData(
               new ProgramMenu { ID = "IVM_IN", Name = "Inventory In", ProgramFolderID = "IVM", ControllerRouteAttribute = "inventory-in", ActionRouteAttribute = "index", SequenceNo = 10 },
               new ProgramMenu { ID = "IVM_OUT", Name = "Inventory Out", ProgramFolderID = "IVM", ControllerRouteAttribute = "inventory-out", ActionRouteAttribute = "index", SequenceNo = 20 }
               );
        }

        private static void SeedDataDictionaryProgramMenus(ModelBuilder modelBuilder)
        {
            //create Sample Management folder 
            modelBuilder.Entity<ProgramFolder>().HasData(
               new ProgramFolder { ID = "DIC", Name = "Data Dictionary",  RouteAttribute = "data-dictionary", SequenceNo = 20 }
               );

            //create Sample Management menu 
            modelBuilder.Entity<ProgramMenu>().HasData(
               new ProgramMenu { ID = "DIC_UNIT", Name = "Unit", ProgramFolderID = "DIC", ControllerRouteAttribute = "unit", ActionRouteAttribute = "index", SequenceNo = 10 },
               new ProgramMenu { ID = "DIC_SUP", Name = "Supplier", ProgramFolderID = "DIC", ControllerRouteAttribute = "supplier", ActionRouteAttribute = "index", SequenceNo = 20 },
               new ProgramMenu { ID = "DIC_MANU", Name = "Manufacturer", ProgramFolderID = "DIC", ControllerRouteAttribute = "manufacturer", ActionRouteAttribute = "index", SequenceNo = 30 },
               new ProgramMenu { ID = "DIC_ITEM", Name = "Item Master", ProgramFolderID = "DIC", ControllerRouteAttribute = "item-master", ActionRouteAttribute = "index", SequenceNo = 40 },
               new ProgramMenu { ID = "DIC_ITEMGRP", Name = "Item Group", ProgramFolderID = "DIC", ControllerRouteAttribute = "item-group", ActionRouteAttribute = "index", SequenceNo = 50 });
        }

        private static void SeedUserManagementProgramMenus(ModelBuilder modelBuilder)
        {
            //create Sample Management folder 
            modelBuilder.Entity<ProgramFolder>().HasData(
               new ProgramFolder { ID = "UM", Name = "User Management", RouteAttribute = "user-management", SequenceNo = 30 }
               );

            //create Sample Management menu 
            modelBuilder.Entity<ProgramMenu>().HasData(
               new ProgramMenu { ID = "UM_USERACC", Name = "User Account", ProgramFolderID = "UM", ControllerRouteAttribute = "user-account", ActionRouteAttribute = "index", SequenceNo = 10 },
               new ProgramMenu { ID = "UM_USERGRP", Name = "User Group", ProgramFolderID = "UM", ControllerRouteAttribute = "user-group", ActionRouteAttribute = "index", SequenceNo = 20 },
               new ProgramMenu { ID = "UM_USERPROF", Name = "User Profile", ProgramFolderID = "UM", ControllerRouteAttribute = "user-profile", ActionRouteAttribute = "index", SequenceNo = 30 });
        }

        private static void CreateSystemAdminGroupWithUserPrograms(ModelBuilder modelBuilder)
        {
            //create Result Management folder 
            modelBuilder.Entity<UserGroup>().HasData(
               new UserGroup { ID = "ADMIN", Name = "SYSTEM ADMINISTRATORS"}
               );

            //create Result Management menu 
            modelBuilder.Entity<UserGroupProgram>().HasData(
               //Inventory Management
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "IVM_IN" },
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "IVM_OUT" },

               //Data Dictionary
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "DIC_UNIT" },
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "DIC_SUP" },
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "DIC_MANU" },
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "DIC_ITEM" },
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "DIC_ITEMGRP" },

               //User Management
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "UM_USERACC" },
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "UM_USERGRP" },
               new UserGroupProgram { UserGroupID = "ADMIN", ProgramMenuID = "UM_USERPROF" }
               );
        }
    }
}
