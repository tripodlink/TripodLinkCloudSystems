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


            //Program Menus
            SeedProgramRoots(modelBuilder);
            //Genlab
            SeedGenlabSampleManagementProgramMenus(modelBuilder);
            SeedGenlabResultManagementProgramMenus(modelBuilder);
            //Blood Bank
            SeedBloodBankSampleManagementProgramMenus(modelBuilder);
            SeedBloodBankResultManagementProgramMenus(modelBuilder);


            //Dictionary Seeds
            SeedClinicians(modelBuilder);
            SeedOrderItems(modelBuilder);


            //User Group Seeds
            modelBuilder.Entity<UserGroupProgram>().HasKey(c => new {c.UserGroupID, c.ProgramMenuID });
            CreateGenlabSystemAdminGroupWithUserPrograms(modelBuilder);
            CreateBloodBankSystemAdminGroupWithUserPrograms(modelBuilder);

        }


        private static void SeedCompany(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasData(
               new Company { ID = "000", CompanyID = "NEW", CompanyName = "NEW CLOUD CMS CUSTOMER" }
               );
        }


        private static void SeedEventTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventTable>().HasData(
                new EventTable { Code = "TRX_ADD", Description = "Add transaction" },
                new EventTable { Code = "TRX_UPDATE", Description = "Update transaction" },
                new EventTable { Code = "TRX_VOID", Description = "Void transaction" },
                new EventTable { Code = "TRX_POST", Description = "Post transaction" },
                new EventTable { Code = "TRX_PAY", Description = "Pay transaction" },
                new EventTable { Code = "TRX_UNDO_PAY", Description = "Undo pay transaction" },
                new EventTable { Code = "TRX_DELETE", Description = "Delete transaction" }
                );
        }

        private static void SeedClinicians(ModelBuilder modelBuilder)
        {
            //create unique index 
            modelBuilder.Entity<Clinician>().HasIndex(idx => new { idx.Name }).IsUnique(true).HasName("IDX_NAME");
            modelBuilder.Entity<Clinician>().HasData(
               new Clinician { ID = "FUR", Name = "FLORANTE U. REGUIS, MD", Email = "reguis.florante@gmail.com", MobileNo = "09171447107" },
               new Clinician { ID = "FGC", Name = "FROILAN G. CUIZON, MD", Email = "cuizonfg@gmail.com", MobileNo = "09171447170" }
               );
        }

        private static void SeedOrderItems(ModelBuilder modelBuilder)
        {
            //create unique index 
            modelBuilder.Entity<Item>().HasIndex(idx => new { idx.Name }).IsUnique(false).HasName("IDX_NAME");
            modelBuilder.Entity<Item>().HasIndex(idx => new { idx.Category }).IsUnique(false).HasName("IDX_CATEGORY");
            modelBuilder.Entity<Item>().HasData(
               new Item { Code = "CBC", Name = "COMPLETE BLOOD COUNT", Category = "S", CostCenter = "LAB", Type = "T", HostCode = "CBC", IsEditablePrice = false, IsActive = true, Remarks = "", CreatedOn = DateTime.Now, CreatedBy = "SYSAD", UpdatedOn = DateTime.Now, UpdatedBy = "SYSAD" },
               new Item { Code = "FBS", Name = "FASTING BLOOD SUGAR", Category = "S", CostCenter = "LAB", Type = "T", HostCode = "FBS", IsEditablePrice = false, IsActive = true, Remarks = "", CreatedOn = DateTime.Now, CreatedBy = "SYSAD", UpdatedOn = DateTime.Now, UpdatedBy = "SYSAD" },
               new Item { Code = "PRE-EMP-1", Name = "PRE-EMPLOYMENT PACKAGE #1", Category = "P", CostCenter = "0", Type = "0", HostCode = "PRE-EMP1", IsEditablePrice = false, IsActive = true, Remarks = "", CreatedOn = DateTime.Now, CreatedBy = "SYSAD", UpdatedOn = DateTime.Now, UpdatedBy = "SYSAD" },
                new Item { Code = "URINA", Name = "URINALYSIS", Category = "S", CostCenter = "LAB", Type = "T", HostCode = "URINA", IsEditablePrice = false, IsActive = true, Remarks = "", CreatedOn = DateTime.Now, CreatedBy = "SYSAD", UpdatedOn = DateTime.Now, UpdatedBy = "SYSAD" }

               );
        }

        private static void SeedUserAccounts(ModelBuilder modelBuilder)
        {
            //create unique index 
            modelBuilder.Entity<UserAccount>().HasData(
               new UserAccount { UserID = "SYSAD", UserName = "SYSTEM ADMINISTRATOR", Password=".00000", IsActive = true , CreatedOn = DateTime.Now , CreatedBy = "SYSTEM", UpdatedOn = DateTime.Now, UpdatedBy = "SYSTEM"  },
               new UserAccount { UserID = "FUR", UserName = "FLORANTE U. REGUIS", Password = ".00000", IsActive = true, CreatedOn = DateTime.Now, CreatedBy = "SYSTEM", UpdatedOn = DateTime.Now, UpdatedBy = "SYSTEM" },
               new UserAccount { UserID = "FGC", UserName = "FROILAN G. CUIZON", Password = ".00000", IsActive = true, CreatedOn = DateTime.Now, CreatedBy = "SYSTEM", UpdatedOn = DateTime.Now, UpdatedBy = "SYSTEM" },
               new UserAccount { UserID = "MLS", UserName = "MARKWIN L. SORIANO", Password = ".00000", IsActive = true, CreatedOn = DateTime.Now, CreatedBy = "SYSTEM", UpdatedOn = DateTime.Now, UpdatedBy = "SYSTEM" }

               );
        }

        private static void SeedUserAccountGroupForUser_SYSAD(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccountGroup>().HasData(
                new UserAccountGroup { UserAccountID = "SYSAD", UserGroupID = "GENADMIN" },
                new UserAccountGroup { UserAccountID = "SYSAD", UserGroupID = "BBADMIN" }
                );
        }

        private static void SeedProgramRoots(ModelBuilder modelBuilder)
        {
            //create unique index 
            modelBuilder.Entity<ProgramRoot>().HasData(
               new ProgramRoot { ID = "PRG", Name = "Program", SequenceNo = 100  },
               new ProgramRoot { ID = "SM", Name = "System Management", SequenceNo = 200 }
               );
        }

        private static void SeedGenlabSampleManagementProgramMenus(ModelBuilder modelBuilder)
        {
            //create Sample Management folder 
            modelBuilder.Entity<ProgramFolder>().HasData(
               new ProgramFolder { ID = "GENSM", Name = "Sample Management", ProgramRootID = "PRG", ModuleID = "GEN", ModuleRouteAttribute = "gen", RouteAttribute = "sample-management", SequenceNo = 10 }
               );

            //create Sample Management menu 
            modelBuilder.Entity<ProgramMenu>().HasData(
               new ProgramMenu { ID = "GENSM_SAMREC", Name = "Sample Reception", ProgramFolderID = "GENSM", ModuleRouteAttribute = "gen", FolderRouteAttribute = "sample-management", ControllerRouteAttribute = "sample-reception", ActionRouteAttribute = "index", SequenceNo = 10 },
               new ProgramMenu { ID = "GENSM_SAMCHK", Name = "Sample Check-In", ProgramFolderID = "GENSM", ModuleRouteAttribute = "gen", FolderRouteAttribute = "sample-management", ControllerRouteAttribute = "sample-checkin", ActionRouteAttribute = "index", SequenceNo = 20 },
               new ProgramMenu { ID = "GENSM_REJSAM", Name = "Reject Sample", ProgramFolderID = "GENSM", ModuleRouteAttribute = "gen", FolderRouteAttribute = "sample-management", ControllerRouteAttribute = "reject -sample", ActionRouteAttribute = "index", SequenceNo = 30 },
               new ProgramMenu { ID = "GENSM_ADDELO", Name = "Add/Delete Lab Order", ProgramFolderID = "GENSM", ModuleRouteAttribute = "gen", FolderRouteAttribute = "sample-management", ControllerRouteAttribute = "add-delete-lab-order", ActionRouteAttribute = "index", SequenceNo = 40 },
               new ProgramMenu { ID = "GENSM_DELLN", Name = "Delete Lab Number", ProgramFolderID = "GENSM", ModuleRouteAttribute = "gen", FolderRouteAttribute = "sample-management", ControllerRouteAttribute = "delete-lab-number", ActionRouteAttribute = "index", SequenceNo = 50 }
               );
        }

        private static void SeedGenlabResultManagementProgramMenus(ModelBuilder modelBuilder)
        {
            //create Result Management folder 
            modelBuilder.Entity<ProgramFolder>().HasData(
               new ProgramFolder { ID = "GENRM", Name = "Result Management", ProgramRootID = "PRG", ModuleID = "GEN", ModuleRouteAttribute = "gen", RouteAttribute = "result-management", SequenceNo = 10 }
               );

            //create Result Management menu 
            modelBuilder.Entity<ProgramMenu>().HasData(
               new ProgramMenu { ID = "GENRM_RESENT", Name = "Result Entry", ProgramFolderID = "GENRM", ModuleRouteAttribute = "gen", FolderRouteAttribute = "result-management", ControllerRouteAttribute = "result-entry", ActionRouteAttribute = "index", SequenceNo = 10 },
               new ProgramMenu { ID = "GENRM_FPATREC", Name = "Find Patient Record", ProgramFolderID = "GENRM", ModuleRouteAttribute = "gen", FolderRouteAttribute = "result-management", ControllerRouteAttribute = "find-patient-record", ActionRouteAttribute = "index", SequenceNo = 20 }
               );
        }

        private static void SeedBloodBankSampleManagementProgramMenus(ModelBuilder modelBuilder)
        {
            //create Sample Management folder 
            modelBuilder.Entity<ProgramFolder>().HasData(
               new ProgramFolder { ID = "BBSM", Name = "Sample Management", ProgramRootID = "PRG", ModuleID = "BB", ModuleRouteAttribute = "gen", RouteAttribute = "sample-management", SequenceNo = 10 }
               );

            //create Sample Management menu 
            modelBuilder.Entity<ProgramMenu>().HasData(
               new ProgramMenu { ID = "BBSM_SAMREC", Name = "Sample Reception", ProgramFolderID = "BBSM", ModuleRouteAttribute = "gen", FolderRouteAttribute = "sample-management" , ControllerRouteAttribute = "sample-reception", ActionRouteAttribute = "index", SequenceNo = 10 },
               new ProgramMenu { ID = "BBSM_SAMCHK", Name = "Sample Check-In", ProgramFolderID = "BBSM", ModuleRouteAttribute = "gen", FolderRouteAttribute = "sample-management", ControllerRouteAttribute = "sample-checkin", ActionRouteAttribute = "index", SequenceNo = 20 },
               new ProgramMenu { ID = "BBSM_REJSAM", Name = "Reject Sample", ProgramFolderID = "BBSM", ModuleRouteAttribute = "gen", FolderRouteAttribute = "sample-management", ControllerRouteAttribute = "reject -sample", ActionRouteAttribute = "index", SequenceNo = 30 },
               new ProgramMenu { ID = "BBSM_ADDELO", Name = "Add/Delete Lab Order", ProgramFolderID = "BBSM", ModuleRouteAttribute = "gen", FolderRouteAttribute = "sample-management", ControllerRouteAttribute = "add-delete-lab-order", ActionRouteAttribute = "index", SequenceNo = 40 },
               new ProgramMenu { ID = "BBSM_DELLN", Name = "Delete Lab Number", ProgramFolderID = "BBSM", ModuleRouteAttribute = "gen", FolderRouteAttribute = "sample-management", ControllerRouteAttribute = "delete-lab-number", ActionRouteAttribute = "index", SequenceNo = 50 }
               );
        }

        private static void SeedBloodBankResultManagementProgramMenus(ModelBuilder modelBuilder)
        {
            //create Result Management folder 
            modelBuilder.Entity<ProgramFolder>().HasData(
               new ProgramFolder { ID = "BBRM", Name = "Result Management", ProgramRootID = "PRG", ModuleID = "BB", ModuleRouteAttribute = "bb", RouteAttribute = "result-management", SequenceNo = 10 }
               );

            //create Result Management menu 
            modelBuilder.Entity<ProgramMenu>().HasData(
               new ProgramMenu { ID = "BBRM_RESENT", Name = "Result Entry", ProgramFolderID = "BBRM", ModuleRouteAttribute = "bb", FolderRouteAttribute = "result-management", ControllerRouteAttribute = "result-entry", ActionRouteAttribute = "index", SequenceNo = 10 },
               new ProgramMenu { ID = "BBRM_FPATREC", Name = "Find Patient Record", ProgramFolderID = "BBRM", ModuleRouteAttribute = "gen", FolderRouteAttribute = "result-management", ControllerRouteAttribute = "find-patient-record", ActionRouteAttribute = "index", SequenceNo = 20 }
               );
        }

        private static void CreateGenlabSystemAdminGroupWithUserPrograms(ModelBuilder modelBuilder)
        {
            //create Result Management folder 
            modelBuilder.Entity<UserGroup>().HasData(
               new UserGroup { ID = "GENADMIN", Name = "SYSTEM ADMINISTRATORS (GENLAB)", ModuleID = "GEN" }
               );

            //create Result Management menu 
            modelBuilder.Entity<UserGroupProgram>().HasData(
               //Sample Reception
               new UserGroupProgram { UserGroupID = "GENADMIN", ProgramMenuID = "BBSM_SAMREC" },
               new UserGroupProgram { UserGroupID = "GENADMIN", ProgramMenuID = "BBSM_SAMCHK" },
               new UserGroupProgram { UserGroupID = "GENADMIN", ProgramMenuID = "BBSM_REJSAM" },
               new UserGroupProgram { UserGroupID = "GENADMIN", ProgramMenuID = "BBSM_ADDELO" },
               new UserGroupProgram { UserGroupID = "GENADMIN", ProgramMenuID = "BBSM_DELLN" },
               //Result Management
               new UserGroupProgram { UserGroupID = "GENADMIN", ProgramMenuID = "BBRM_RESENT" },
               new UserGroupProgram { UserGroupID = "GENADMIN", ProgramMenuID = "BBRM_FPATREC" }
               );
        }

        private static void CreateBloodBankSystemAdminGroupWithUserPrograms(ModelBuilder modelBuilder)
        {
            //create Result Management folder 
            modelBuilder.Entity<UserGroup>().HasData(
               new UserGroup { ID = "BBADMIN", Name = "SYSTEM ADMINISTRATORS (BLOOD BANK)", ModuleID = "BB" }
               );

            //create Result Management menu 
            modelBuilder.Entity<UserGroupProgram>().HasData(
               //Sample Reception
               new UserGroupProgram { UserGroupID = "BBADMIN", ProgramMenuID = "BBSM_SAMREC" },
               new UserGroupProgram { UserGroupID = "BBADMIN", ProgramMenuID = "BBSM_SAMCHK" },
               new UserGroupProgram { UserGroupID = "BBADMIN", ProgramMenuID = "BBSM_REJSAM" },
               new UserGroupProgram { UserGroupID = "BBADMIN", ProgramMenuID = "BBSM_ADDELO" },
               new UserGroupProgram { UserGroupID = "BBADMIN", ProgramMenuID = "BBSM_DELLN" },
               //Result Management
               new UserGroupProgram { UserGroupID = "BBADMIN", ProgramMenuID = "BBRM_RESENT" },
               new UserGroupProgram { UserGroupID = "BBADMIN", ProgramMenuID = "BBRM_FPATREC" }
               );
        }
    }
}
