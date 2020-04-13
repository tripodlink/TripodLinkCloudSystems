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
    [Migration("20200413030704_Add_InventoryIn_trx_header_and_detail_table")]
    partial class Add_InventoryIn_trx_header_and_detail_table
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085");

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

                    b.ToTable("item_group");
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

                    b.ToTable("manufacturer");
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
                        new { ID = "DASH", Name = "Dashboard", RouteAttribute = "dashboard", SequenceNo = 0 },
                        new { ID = "IVM", Name = "Inventory Management", RouteAttribute = "inventory-management", SequenceNo = 10 },
                        new { ID = "DIC", Name = "Data Dictionary", RouteAttribute = "data-dictionary", SequenceNo = 20 },
                        new { ID = "UM", Name = "User Management", RouteAttribute = "user-management", SequenceNo = 30 }
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
                        new { ID = "IVM_IN", Name = "Inventory In", ProgramFolderID = "IVM", RouteAttribute = "inventory-in", SequenceNo = 10 },
                        new { ID = "IVM_OUT", Name = "Inventory Out", ProgramFolderID = "IVM", RouteAttribute = "inventory-out", SequenceNo = 20 },
                        new { ID = "DIC_UNIT", Name = "Unit", ProgramFolderID = "DIC", RouteAttribute = "unit", SequenceNo = 10 },
                        new { ID = "DIC_SUP", Name = "Supplier", ProgramFolderID = "DIC", RouteAttribute = "supplier", SequenceNo = 20 },
                        new { ID = "DIC_MANU", Name = "Manufacturer", ProgramFolderID = "DIC", RouteAttribute = "manufacturer", SequenceNo = 30 },
                        new { ID = "DIC_ITEM", Name = "Item Master", ProgramFolderID = "DIC", RouteAttribute = "item-master", SequenceNo = 40 },
                        new { ID = "DIC_ITEMGRP", Name = "Item Group", ProgramFolderID = "DIC", RouteAttribute = "item-group", SequenceNo = 50 },
                        new { ID = "UM_USERACC", Name = "User Account", ProgramFolderID = "UM", RouteAttribute = "user-account", SequenceNo = 10 },
                        new { ID = "UM_USERGRP", Name = "User Group", ProgramFolderID = "UM", RouteAttribute = "user-group", SequenceNo = 20 },
                        new { ID = "UM_USERPROF", Name = "User Profile", ProgramFolderID = "UM", RouteAttribute = "user-profile", SequenceNo = 30 }
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

                    b.ToTable("supplier");
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
                        new { Code = "100", Description = "Meter" },
                        new { Code = "101", Description = "Kilo" },
                        new { Code = "102", Description = "Gram" }
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
                        new { UserID = "SYSAD", CreatedBy = "SYSTEM", CreatedOn = new DateTime(2020, 4, 13, 11, 7, 4, 20, DateTimeKind.Local), IsActive = (byte)1, Password = ".00000", UpdatedBy = "SYSTEM", UpdatedOn = new DateTime(2020, 4, 13, 11, 7, 4, 21, DateTimeKind.Local), UserName = "SYSTEM ADMINISTRATOR" }
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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("ug_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("user_group");

                    b.HasData(
                        new { ID = "ADMIN", Name = "SYSTEM ADMINISTRATORS" }
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
                        new { UserGroupID = "ADMIN", ProgramMenuID = "IVM_IN" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "IVM_OUT" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "DIC_UNIT" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "DIC_SUP" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "DIC_MANU" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "DIC_ITEM" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "DIC_ITEMGRP" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "UM_USERACC" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "UM_USERGRP" },
                        new { UserGroupID = "ADMIN", ProgramMenuID = "UM_USERPROF" }
                    );
                });
#pragma warning restore 612, 618
        }
    }
}
