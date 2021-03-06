﻿// <auto-generated />
using System;
using CloudImsCommon.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CloudImsCommon.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200912164658_DefectedItemsModelMigration")]
    partial class DefectedItemsModelMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085");

            modelBuilder.Entity("CloudImsCommon.Models.AutoNumber", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("an_type")
                        .HasMaxLength(100);

                    b.Property<string>("Auto_Length")
                        .IsRequired()
                        .HasColumnName("an_auto_length")
                        .HasMaxLength(255);

                    b.Property<string>("Current_year")
                        .IsRequired()
                        .HasColumnName("an_current_year")
                        .HasMaxLength(255);

                    b.Property<string>("Date_Prefix")
                        .IsRequired()
                        .HasColumnName("an_date_prefix")
                        .HasMaxLength(255);

                    b.Property<string>("Last_Value")
                        .IsRequired()
                        .HasColumnName("an_last_value")
                        .HasMaxLength(255);

                    b.Property<string>("Text_Prefix")
                        .IsRequired()
                        .HasColumnName("an_text_prefix")
                        .HasMaxLength(255);

                    b.HasKey("ID");

                    b.ToTable("Auto_Number");

                    b.HasData(
                        new { ID = "INVIN", Auto_Length = "8", Current_year = "2020", Date_Prefix = "YY", Last_Value = "1", Text_Prefix = "TI" },
                        new { ID = "INVOUT", Auto_Length = "8", Current_year = "2020", Date_Prefix = "YY", Last_Value = "1", Text_Prefix = "TO" },
                        new { ID = "INVIN_BID", Auto_Length = "8", Current_year = "2020", Date_Prefix = "YY", Last_Value = "1", Text_Prefix = "BID" }
                    );
                });

            modelBuilder.Entity("CloudImsCommon.Models.Company", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasMaxLength(3);

                    b.Property<string>("CompanyID")
                        .IsRequired()
                        .HasColumnName("company_id")
                        .HasMaxLength(10);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnName("company_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("company");

                    b.HasData(
                        new { ID = "000", CompanyID = "NEW", CompanyName = "NEW CLOUD IMS CUSTOMER" }
                    );
                });

            modelBuilder.Entity("CloudImsCommon.Models.DefectedItemsModel", b =>
                {
                    b.Property<string>("DefectTransactionNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("defect_trxno")
                        .HasMaxLength(100);

                    b.Property<string>("ItemID")
                        .IsRequired()
                        .HasColumnName("item_id")
                        .HasMaxLength(100);

                    b.Property<string>("ItemUnit")
                        .IsRequired()
                        .HasColumnName("item_unit")
                        .HasMaxLength(100);

                    b.Property<string>("LotNumber")
                        .IsRequired()
                        .HasColumnName("lot_number")
                        .HasMaxLength(100);

                    b.Property<double>("Quantity")
                        .HasColumnName("quantity");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnName("remarks")
                        .HasMaxLength(100);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnName("status")
                        .HasMaxLength(10);

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnName("trx_date");

                    b.Property<string>("TransactionNo")
                        .IsRequired()
                        .HasColumnName("trxno")
                        .HasMaxLength(100);

                    b.HasKey("DefectTransactionNo");

                    b.ToTable("defected_items");
                });

            modelBuilder.Entity("CloudImsCommon.Models.Department", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("d_id")
                        .HasMaxLength(15);

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnName("d_depertment_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("DepartmentName")
                        .HasName("IDX_DESCRIPTION");

                    b.ToTable("department");

                    b.HasData(
                        new { ID = "100", DepartmentName = "Default DepartmentName" }
                    );
                });

            modelBuilder.Entity("CloudImsCommon.Models.EventTable", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("et_code")
                        .HasMaxLength(20);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("et_description")
                        .HasMaxLength(100);

                    b.Property<string>("Remarks")
                        .HasColumnName("et_remarks")
                        .HasMaxLength(500);

                    b.HasKey("Code");

                    b.ToTable("event_table");

                    b.HasData(
                        new { Code = "IV_IN", Description = "Inventory In" }
                    );
                });

            modelBuilder.Entity("CloudImsCommon.Models.InventoryInTrxDetail", b =>
                {
                    b.Property<string>("TransactionNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("trxno")
                        .HasMaxLength(100);

                    b.Property<double>("Count")
                        .HasColumnName("count");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnName("exp_date");

                    b.Property<string>("ItemID")
                        .IsRequired()
                        .HasColumnName("item_id")
                        .HasMaxLength(100);

                    b.Property<string>("LotNumber")
                        .IsRequired()
                        .HasColumnName("lotno")
                        .HasMaxLength(100);

                    b.Property<int>("Quantity")
                        .HasColumnName("quantity");

                    b.Property<double>("RemainigCount")
                        .HasColumnName("remaning_count");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnName("unit")
                        .HasMaxLength(100);

                    b.HasKey("TransactionNo");

                    b.ToTable("inventoryin_trx_detail");
                });

            modelBuilder.Entity("CloudImsCommon.Models.InventoryInTrxHeader", b =>
                {
                    b.Property<string>("TransactionNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("trxno")
                        .HasMaxLength(100);

                    b.Property<string>("DocumnetNo")
                        .HasColumnName("doc_number")
                        .HasMaxLength(100);

                    b.Property<string>("InvoiceNo")
                        .HasColumnName("invoice_number")
                        .HasMaxLength(100);

                    b.Property<string>("PONumber")
                        .HasColumnName("po_number")
                        .HasMaxLength(100);

                    b.Property<string>("ReceivedBy")
                        .HasColumnName("rcvd_by")
                        .HasMaxLength(100);

                    b.Property<DateTime>("ReceivedDate")
                        .HasColumnName("rcvd_date");

                    b.Property<string>("ReferenceNo")
                        .HasColumnName("ref_number")
                        .HasMaxLength(100);

                    b.Property<string>("Remarks")
                        .HasColumnName("remarks")
                        .HasMaxLength(100);

                    b.Property<string>("Supplier")
                        .HasColumnName("supplier")
                        .HasMaxLength(100);

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnName("trx_date");

                    b.HasKey("TransactionNo");

                    b.ToTable("inventoryin_trx_header");
                });

            modelBuilder.Entity("CloudImsCommon.Models.InventoryOutTrxDetail", b =>
                {
                    b.Property<string>("TransactionNo")
                        .HasColumnName("itoh_trxno")
                        .HasMaxLength(100);

                    b.Property<string>("ItemID")
                        .HasColumnName("itoh_item_id")
                        .HasMaxLength(100);

                    b.Property<string>("In_TrxNo")
                        .HasColumnName("itoh_in_trxno")
                        .HasMaxLength(100);

                    b.Property<double>("MinCount")
                        .HasColumnName("itoh_mincount");

                    b.Property<double>("Quantity")
                        .HasColumnName("itoh_quantity");

                    b.Property<string>("Remarks")
                        .HasColumnName("itoh_remarks")
                        .HasMaxLength(300);

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnName("itoh_unit")
                        .HasMaxLength(100);

                    b.HasKey("TransactionNo", "ItemID", "In_TrxNo");

                    b.HasAlternateKey("In_TrxNo", "ItemID", "TransactionNo");

                    b.ToTable("inventoryout_trx_detail");
                });

            modelBuilder.Entity("CloudImsCommon.Models.InventoryOutTrxHeader", b =>
                {
                    b.Property<string>("TransactionNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("itoh_trxno")
                        .HasMaxLength(100);

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnName("itoh_department")
                        .HasMaxLength(200);

                    b.Property<string>("IssuedBy")
                        .IsRequired()
                        .HasColumnName("itoh_issued_by")
                        .HasMaxLength(100);

                    b.Property<DateTime>("IssuedDate")
                        .HasColumnName("itoh_issued_date");

                    b.Property<string>("ReceivedBy")
                        .IsRequired()
                        .HasColumnName("itoh_rcvd_by")
                        .HasMaxLength(100);

                    b.Property<string>("ReferenceNo")
                        .IsRequired()
                        .HasColumnName("itoh_ref_number")
                        .HasMaxLength(100);

                    b.Property<string>("Remarks")
                        .HasColumnName("itoh_remarks")
                        .HasMaxLength(100);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnName("itoh_status")
                        .HasMaxLength(100);

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnName("itoh_trx_date");

                    b.HasKey("TransactionNo");

                    b.ToTable("inventoryout_trx_header");
                });

            modelBuilder.Entity("CloudImsCommon.Models.ItemGroup", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ig_id")
                        .HasMaxLength(10);

                    b.Property<string>("ItemGroupName")
                        .IsRequired()
                        .HasColumnName("ig_item_group_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("ItemGroupName")
                        .HasName("IDX_DESCRIPTION");

                    b.ToTable("item_group");

                    b.HasData(
                        new { ID = "100", ItemGroupName = "Default ItemGroup" }
                    );
                });

            modelBuilder.Entity("CloudImsCommon.Models.ItemMaster", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("im_id")
                        .HasMaxLength(10);

                    b.Property<string>("ItemGroup")
                        .IsRequired()
                        .HasColumnName("im_item_group")
                        .HasMaxLength(250);

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnName("im_item_name")
                        .HasMaxLength(250);

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnName("im_manufact")
                        .HasMaxLength(100);

                    b.Property<string>("Supplier")
                        .IsRequired()
                        .HasColumnName("im_supp")
                        .HasMaxLength(100);

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnName("im_unit")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("item_master");

                    b.HasData(
                        new { ID = "01", ItemGroup = "100", ItemName = "Default Item", Manufacturer = "100", Supplier = "100", Unit = "102" }
                    );
                });

            modelBuilder.Entity("CloudImsCommon.Models.ItemMasterUnit", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnName("imu_id")
                        .HasMaxLength(100);

                    b.Property<string>("itemMasterUnitUnit")
                        .HasColumnName("imu_unit")
                        .HasMaxLength(250);

                    b.Property<string>("itemMasterUnitConversion")
                        .HasColumnName("im_item_conversion")
                        .HasMaxLength(250);

                    b.HasKey("ID", "itemMasterUnitUnit");

                    b.ToTable("item_master_unit");

                    b.HasData(
                        new { ID = "01", itemMasterUnitUnit = "102", itemMasterUnitConversion = "1" }
                    );
                });

            modelBuilder.Entity("CloudImsCommon.Models.Manufacturer", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("m_id")
                        .HasMaxLength(10);

                    b.Property<string>("ManufactName")
                        .IsRequired()
                        .HasColumnName("m_item_group_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("ManufactName")
                        .HasName("IDX_DESCRIPTION");

                    b.ToTable("manufacturer");

                    b.HasData(
                        new { ID = "100", ManufactName = "Default Manufacturer" }
                    );
                });

            modelBuilder.Entity("CloudImsCommon.Models.ProgramFolder", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("pf_folder_id")
                        .HasMaxLength(10);

                    b.Property<string>("Icon")
                        .HasColumnName("pf_icon")
                        .HasMaxLength(100);

                    b.Property<string>("IconProvider")
                        .HasColumnName("pf_icon_provider")
                        .HasMaxLength(20);

                    b.Property<string>("IconType")
                        .HasColumnName("pf_icon_type")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("pf_folder_name")
                        .HasMaxLength(100);

                    b.Property<string>("RouteAttribute")
                        .IsRequired()
                        .HasColumnName("pf_folder_route")
                        .HasMaxLength(25);

                    b.Property<int>("SequenceNo")
                        .HasColumnName("pf_seqno");

                    b.HasKey("ID");

                    b.ToTable("program_folder");

                    b.HasData(
                        new { ID = "IVM", Icon = "fa fa-truck", Name = "Inventory Management", RouteAttribute = "inventory-management", SequenceNo = 10 },
                        new { ID = "DIC", Icon = "fa fa-file-code-o", Name = "Data Dictionary", RouteAttribute = "data-dictionary", SequenceNo = 20 },
                        new { ID = "UM", Icon = "fa fa-user-circle-o", Name = "User Management", RouteAttribute = "user-management", SequenceNo = 30 },
                        new { ID = "DASH", Icon = "fa fa-tachometer", Name = "Dashboard", RouteAttribute = "dashboard", SequenceNo = 10 },
                        new { ID = "RPT", Icon = "fa fa-file-text", Name = "Report Management", RouteAttribute = "report-management", SequenceNo = 10 }
                    );
                });

            modelBuilder.Entity("CloudImsCommon.Models.ProgramMenu", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("pm_id")
                        .HasMaxLength(20);

                    b.Property<string>("IconName")
                        .HasColumnName("pm_icon_name")
                        .HasMaxLength(100);

                    b.Property<string>("IconProvider")
                        .HasColumnName("pm_icon_provider")
                        .HasMaxLength(20);

                    b.Property<string>("IconType")
                        .HasColumnName("pm_icon_type")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("pm_name")
                        .HasMaxLength(100);

                    b.Property<string>("ProgramFolderID")
                        .IsRequired()
                        .HasColumnName("pm_folder")
                        .HasMaxLength(10);

                    b.Property<string>("RouteAttribute")
                        .IsRequired()
                        .HasColumnName("pm_route")
                        .HasMaxLength(25);

                    b.Property<int>("SequenceNo")
                        .HasColumnName("pm_seqno");

                    b.HasKey("ID");

                    b.ToTable("program_menu");

                    b.HasData(
                        new { ID = "IVM_IN", IconName = "fa fa-sign-in", Name = "Inventory In", ProgramFolderID = "IVM", RouteAttribute = "inventory-in", SequenceNo = 10 },
                        new { ID = "IVM_OUT", IconName = "fa fa-sign-out", Name = "Inventory Out", ProgramFolderID = "IVM", RouteAttribute = "inventory-out", SequenceNo = 20 },
                        new { ID = "IVM_DEFECT", IconName = "fa fa-times", Name = "Defected Items", ProgramFolderID = "IVM", RouteAttribute = "defected-items", SequenceNo = 30 },
                        new { ID = "DIC_UNIT", IconName = "fa fa-balance-scale", Name = "Unit", ProgramFolderID = "DIC", RouteAttribute = "unit-code", SequenceNo = 60 },
                        new { ID = "DIC_SUP", IconName = "fa fa-truck", Name = "Supplier", ProgramFolderID = "DIC", RouteAttribute = "supplier", SequenceNo = 50 },
                        new { ID = "DIC_MANU", IconName = "fa fa-industry", Name = "Manufacturer", ProgramFolderID = "DIC", RouteAttribute = "manufacturer", SequenceNo = 40 },
                        new { ID = "DIC_ITEM", IconName = "fa fa-cube", Name = "Item Master", ProgramFolderID = "DIC", RouteAttribute = "item-master", SequenceNo = 20 },
                        new { ID = "DIC_ITEMGRP", IconName = "fa fa-cubes", Name = "Item Group", ProgramFolderID = "DIC", RouteAttribute = "item-group", SequenceNo = 30 },
                        new { ID = "DIC_DEP", IconName = "fa fa-home", Name = "Department", ProgramFolderID = "DIC", RouteAttribute = "department", SequenceNo = 10 },
                        new { ID = "UM_USERACC", IconName = "fa fa-users", Name = "User Account", ProgramFolderID = "UM", RouteAttribute = "user-account", SequenceNo = 10 },
                        new { ID = "UM_USERGRP", IconName = "fa fa-sitemap", Name = "User Group", ProgramFolderID = "UM", RouteAttribute = "user-group", SequenceNo = 20 },
                        new { ID = "UM_USERPROF", IconName = "fa fa-user", Name = "User Profile", ProgramFolderID = "UM", RouteAttribute = "user-profile", SequenceNo = 30 },
                        new { ID = "DASH", IconName = "fa fa-tachometer", Name = "Dashboard", ProgramFolderID = "DASH", RouteAttribute = "dashboard", SequenceNo = 10 },
                        new { ID = "RPT_IVM_IN", IconName = "fa fa-line-chart", Name = "(Report) Inventory In", ProgramFolderID = "RPT", RouteAttribute = "report-inventory-in", SequenceNo = 10 },
                        new { ID = "RPT_IVM_OUT", IconName = "fa fa-area-chart", Name = "(Report) Inventory Out", ProgramFolderID = "RPT", RouteAttribute = "report-inventory-out", SequenceNo = 20 }
                    );
                });

            modelBuilder.Entity("CloudImsCommon.Models.Supplier", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("sup_id")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("sup_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .HasName("IDX_DESCRIPTION");

                    b.ToTable("supplier");

                    b.HasData(
                        new { ID = "100", Name = "Default Supplier" }
                    );
                });

            modelBuilder.Entity("CloudImsCommon.Models.UnitCode", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("uc_code")
                        .HasMaxLength(3);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("uc_description")
                        .HasMaxLength(20);

                    b.Property<string>("ShortDescription")
                        .HasColumnName("uc_short_description")
                        .HasMaxLength(10);

                    b.HasKey("Code");

                    b.HasIndex("Description")
                        .HasName("IDX_DESCRIPTION");

                    b.ToTable("unit_code");

                    b.HasData(
                        new { Code = "100", Description = "Box" },
                        new { Code = "101", Description = "Tray" },
                        new { Code = "102", Description = "Pieces" }
                    );
                });

            modelBuilder.Entity("CloudImsCommon.Models.UserAccount", b =>
                {
                    b.Property<string>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ua_user_id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnName("im_created_by")
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("im_created_on");

                    b.Property<byte>("IsActive")
                        .HasColumnName("ua_is_active")
                        .HasColumnType("TINYINT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("ua_password")
                        .HasMaxLength(100);

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnName("im_updated_by")
                        .HasMaxLength(20);

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnName("im_updated_on");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnName("ua_user_name");

                    b.HasKey("UserID");

                    b.ToTable("user_account");

                    b.HasData(
                        new { UserID = "SYSAD", CreatedBy = "SYSTEM", CreatedOn = new DateTime(2020, 9, 13, 0, 46, 57, 954, DateTimeKind.Local), IsActive = (byte)1, Password = ".00000", UpdatedBy = "SYSTEM", UpdatedOn = new DateTime(2020, 9, 13, 0, 46, 57, 955, DateTimeKind.Local), UserName = "SYSTEM ADMINISTRATOR" }
                    );
                });

            modelBuilder.Entity("CloudImsCommon.Models.UserAccountGroup", b =>
                {
                    b.Property<string>("UserAccountID")
                        .HasColumnName("uag_user_id")
                        .HasMaxLength(20);

                    b.Property<string>("UserGroupID")
                        .HasColumnName("uag_group_id")
                        .HasMaxLength(20);

                    b.HasKey("UserAccountID", "UserGroupID");

                    b.ToTable("user_account_group");

                    b.HasData(
                        new { UserAccountID = "SYSAD", UserGroupID = "ADMIN" }
                    );
                });

            modelBuilder.Entity("CloudImsCommon.Models.UserGroup", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ug_id")
                        .HasMaxLength(20);

                    b.Property<byte>("IsApprover")
                        .HasColumnName("ug_is_approver")
                        .HasColumnType("TINYINT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("ug_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("user_group");

                    b.HasData(
                        new { ID = "ADMIN", IsApprover = (byte)1, Name = "SYSTEM ADMINISTRATORS" }
                    );
                });

            modelBuilder.Entity("CloudImsCommon.Models.UserGroupProgram", b =>
                {
                    b.Property<string>("UserGroupID")
                        .HasColumnName("ugp_user_group")
                        .HasMaxLength(20);

                    b.Property<string>("ProgramMenuID")
                        .HasColumnName("ugp_program_id")
                        .HasMaxLength(20);

                    b.HasKey("UserGroupID", "ProgramMenuID");

                    b.HasAlternateKey("ProgramMenuID", "UserGroupID");

                    b.ToTable("user_group_program");

                    b.HasData(
                        new { UserGroupID = "ADMIN", ProgramMenuID = "RPT_IVM_IN" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "RPT_IVM_OUT" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "IVM_IN" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "IVM_OUT" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "DIC_UNIT" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "DIC_SUP" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "DIC_MANU" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "DIC_ITEM" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "DIC_ITEMGRP" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "DIC_DEP" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "DASH" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "UM_USERACC" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "UM_USERGRP" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "UM_USERPROF" }
                    );
                });
#pragma warning restore 612, 618
        }
    }
}
