﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using CloudImsCommon.Database;

namespace CloudImsCommon.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200101122808_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085");

            modelBuilder.Entity("CloudImsCommon.Models.Clinician", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("clin_id")
                        .HasMaxLength(20);

                    b.Property<string>("Email")
                        .HasColumnName("clin_email")
                        .HasMaxLength(100);

                    b.Property<string>("MobileNo")
                        .HasColumnName("clin_mobile_no")
                        .HasMaxLength(40);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("clin_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("IDX_NAME");

                    b.ToTable("clinician");

                    b.HasData(
                        new { ID = "FUR", Email = "reguis.florante@gmail.com", MobileNo = "09171447107", Name = "FLORANTE U. REGUIS, MD" },
                        new { ID = "FGC", Email = "cuizonfg@gmail.com", MobileNo = "09171447170", Name = "FROILAN G. CUIZON, MD" }
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
                        new { ID = "000", CompanyID = "NEW", CompanyName = "NEW CLOUD CMS CUSTOMER" }
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
                        new { Code = "TRX_ADD", Description = "Add transaction" },
                        new { Code = "TRX_UPDATE", Description = "Update transaction" },
                        new { Code = "TRX_VOID", Description = "Void transaction" },
                        new { Code = "TRX_POST", Description = "Post transaction" },
                        new { Code = "TRX_PAY", Description = "Pay transaction" },
                        new { Code = "TRX_UNDO_PAY", Description = "Undo pay transaction" },
                        new { Code = "TRX_DELETE", Description = "Delete transaction" }
                    );
                });

            modelBuilder.Entity("CloudImsCommon.Models.Item", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("im_code")
                        .HasMaxLength(10);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnName("im_category")
                        .HasMaxLength(1);

                    b.Property<string>("CostCenter")
                        .IsRequired()
                        .HasColumnName("im_cost_center")
                        .HasMaxLength(10);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnName("im_created_by")
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("im_created_on");

                    b.Property<string>("HostCode")
                        .IsRequired()
                        .HasColumnName("im_host_code")
                        .HasMaxLength(20);

                    b.Property<byte>("IsActive")
                        .HasColumnName("im_active")
                        .HasColumnType("TINYINT");

                    b.Property<byte>("IsEditablePrice")
                        .HasColumnName("im_editable_price")
                        .HasColumnType("TINYINT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("im_name")
                        .HasMaxLength(100);

                    b.Property<string>("Remarks")
                        .HasColumnName("im_remarks")
                        .HasMaxLength(500);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnName("im_type")
                        .HasMaxLength(3);

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnName("im_updated_by")
                        .HasMaxLength(20);

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnName("im_updated_on");

                    b.HasKey("Code");

                    b.HasIndex("Category")
                        .HasName("IDX_CATEGORY");

                    b.HasIndex("Name")
                        .HasName("IDX_NAME");

                    b.ToTable("item_master");

                    b.HasData(
                        new { Code = "CBC", Category = "S", CostCenter = "LAB", CreatedBy = "SYSAD", CreatedOn = new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local), HostCode = "CBC", IsActive = (byte)1, IsEditablePrice = (byte)0, Name = "COMPLETE BLOOD COUNT", Remarks = "", Type = "T", UpdatedBy = "SYSAD", UpdatedOn = new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local) },
                        new { Code = "FBS", Category = "S", CostCenter = "LAB", CreatedBy = "SYSAD", CreatedOn = new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local), HostCode = "FBS", IsActive = (byte)1, IsEditablePrice = (byte)0, Name = "FASTING BLOOD SUGAR", Remarks = "", Type = "T", UpdatedBy = "SYSAD", UpdatedOn = new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local) },
                        new { Code = "PRE-EMP-1", Category = "P", CostCenter = "0", CreatedBy = "SYSAD", CreatedOn = new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local), HostCode = "PRE-EMP1", IsActive = (byte)1, IsEditablePrice = (byte)0, Name = "PRE-EMPLOYMENT PACKAGE #1", Remarks = "", Type = "0", UpdatedBy = "SYSAD", UpdatedOn = new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local) },
                        new { Code = "URINA", Category = "S", CostCenter = "LAB", CreatedBy = "SYSAD", CreatedOn = new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local), HostCode = "URINA", IsActive = (byte)1, IsEditablePrice = (byte)0, Name = "URINALYSIS", Remarks = "", Type = "T", UpdatedBy = "SYSAD", UpdatedOn = new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local) }
                    );
                });

            modelBuilder.Entity("CloudImsCommon.Models.UserAccount", b =>
                {
                    b.Property<string>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ua_user_id")
                        .HasMaxLength(20);

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
                        .HasColumnName("ua_user_name")
                        .HasMaxLength(100);

                    b.HasKey("UserID");

                    b.ToTable("user_account");

                    b.HasData(
                        new { UserID = "SYSAD", CreatedBy = "SYSTEM", CreatedOn = new DateTime(2020, 1, 1, 20, 28, 7, 935, DateTimeKind.Local), IsActive = (byte)1, Password = ".00000", UpdatedBy = "SYSTEM", UpdatedOn = new DateTime(2020, 1, 1, 20, 28, 7, 936, DateTimeKind.Local), UserName = "SYSTEM ADMINISTRATOR" },
                        new { UserID = "FUR", CreatedBy = "SYSTEM", CreatedOn = new DateTime(2020, 1, 1, 20, 28, 7, 937, DateTimeKind.Local), IsActive = (byte)1, Password = ".00000", UpdatedBy = "SYSTEM", UpdatedOn = new DateTime(2020, 1, 1, 20, 28, 7, 937, DateTimeKind.Local), UserName = "FLORANTE U. REGUIS" }
                    );
                });
#pragma warning restore 612, 618
        }
    }
}
