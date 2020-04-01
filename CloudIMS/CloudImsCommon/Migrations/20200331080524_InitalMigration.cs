using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudImsCommon.Migrations
{
    public partial class InitalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 3, nullable: false),
                    company_id = table.Column<string>(maxLength: 10, nullable: false),
                    company_name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "event_table",
                columns: table => new
                {
                    et_code = table.Column<string>(maxLength: 20, nullable: false),
                    et_description = table.Column<string>(maxLength: 100, nullable: false),
                    et_remarks = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_table", x => x.et_code);
                });

            migrationBuilder.CreateTable(
                name: "item_group",
                columns: table => new
                {
                    ig_id = table.Column<string>(maxLength: 10, nullable: false),
                    ig_item_group_name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_group", x => x.ig_id);
                });

            migrationBuilder.CreateTable(
                name: "item_master",
                columns: table => new
                {
                    im_id = table.Column<string>(maxLength: 10, nullable: false),
                    im_item_group = table.Column<string>(maxLength: 250, nullable: false),
                    im_item_name = table.Column<string>(maxLength: 250, nullable: false),
                    im_unit = table.Column<string>(maxLength: 100, nullable: false),
                    im_supp = table.Column<string>(maxLength: 100, nullable: false),
                    im_manufact = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_master", x => x.im_id);
                });

            migrationBuilder.CreateTable(
                name: "item_master_unit",
                columns: table => new
                {
                    imu_id = table.Column<string>(maxLength: 100, nullable: false),
                    imu_unit = table.Column<string>(maxLength: 250, nullable: false),
                    im_item_conversion = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_master_unit", x => new { x.imu_id, x.imu_unit });
                });

            migrationBuilder.CreateTable(
                name: "manufacturer",
                columns: table => new
                {
                    m_id = table.Column<string>(maxLength: 10, nullable: false),
                    m_item_group_name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manufacturer", x => x.m_id);
                });

            migrationBuilder.CreateTable(
                name: "program_folder",
                columns: table => new
                {
                    pf_folder_id = table.Column<string>(maxLength: 100, nullable: false),
                    pf_folder_name = table.Column<string>(maxLength: 255, nullable: false),
                    pf_folder_route = table.Column<string>(maxLength: 255, nullable: false),
                    pf_icon_type = table.Column<string>(maxLength: 255, nullable: true),
                    pf_icon_provider = table.Column<string>(maxLength: 255, nullable: true),
                    pf_icon = table.Column<string>(maxLength: 255, nullable: true),
                    pf_seqno = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_program_folder", x => x.pf_folder_id);
                });

            migrationBuilder.CreateTable(
                name: "program_menu",
                columns: table => new
                {
                    pm_id = table.Column<string>(maxLength: 20, nullable: false),
                    pm_name = table.Column<string>(maxLength: 100, nullable: false),
                    pm_folder = table.Column<string>(maxLength: 10, nullable: false),
                    pm_controller_route = table.Column<string>(maxLength: 25, nullable: false),
                    pm_action_route = table.Column<string>(maxLength: 25, nullable: false),
                    pm_icon_type = table.Column<string>(maxLength: 10, nullable: true),
                    pm_icon_provider = table.Column<string>(maxLength: 20, nullable: true),
                    pm_icon_name = table.Column<string>(maxLength: 100, nullable: true),
                    pm_seqno = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_program_menu", x => x.pm_id);
                });

            migrationBuilder.CreateTable(
                name: "supplier",
                columns: table => new
                {
                    sup_id = table.Column<string>(maxLength: 10, nullable: false),
                    sup_name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplier", x => x.sup_id);
                });

            migrationBuilder.CreateTable(
                name: "unit_code",
                columns: table => new
                {
                    uc_code = table.Column<string>(maxLength: 3, nullable: false),
                    uc_description = table.Column<string>(maxLength: 20, nullable: false),
                    uc_short_description = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unit_code", x => x.uc_code);
                });

            migrationBuilder.CreateTable(
                name: "user_account",
                columns: table => new
                {
                    ua_user_id = table.Column<string>(nullable: false),
                    ua_user_name = table.Column<string>(nullable: false),
                    ua_password = table.Column<string>(maxLength: 100, nullable: false),
                    ua_is_active = table.Column<byte>(type: "TINYINT", nullable: false),
                    im_created_on = table.Column<DateTime>(nullable: false),
                    im_created_by = table.Column<string>(maxLength: 20, nullable: false),
                    im_updated_on = table.Column<DateTime>(nullable: false),
                    im_updated_by = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_account", x => x.ua_user_id);
                });

            migrationBuilder.CreateTable(
                name: "user_account_group",
                columns: table => new
                {
                    uag_user_id = table.Column<string>(maxLength: 20, nullable: false),
                    uag_group_id = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_account_group", x => new { x.uag_user_id, x.uag_group_id });
                });

            migrationBuilder.CreateTable(
                name: "user_group",
                columns: table => new
                {
                    ug_id = table.Column<string>(maxLength: 20, nullable: false),
                    ug_name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_group", x => x.ug_id);
                });

            migrationBuilder.CreateTable(
                name: "user_group_program",
                columns: table => new
                {
                    ugp_user_group = table.Column<string>(maxLength: 20, nullable: false),
                    ugp_program_id = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_group_program", x => new { x.ugp_user_group, x.ugp_program_id });
                    table.UniqueConstraint("AK_user_group_program_ugp_program_id_ugp_user_group", x => new { x.ugp_program_id, x.ugp_user_group });
                });

            migrationBuilder.InsertData(
                table: "company",
                columns: new[] { "id", "company_id", "company_name" },
                values: new object[] { "000", "NEW", "NEW CLOUD IMS CUSTOMER" });

            migrationBuilder.InsertData(
                table: "event_table",
                columns: new[] { "et_code", "et_description", "et_remarks" },
                values: new object[] { "IV_IN", "Inventory In", null });

            migrationBuilder.InsertData(
                table: "program_folder",
                columns: new[] { "pf_folder_id", "pf_icon", "pf_icon_provider", "pf_icon_type", "pf_folder_name", "pf_folder_route", "pf_seqno" },
                values: new object[,]
                {
                    { "DASH", null, null, null, "Dashboard", "dashboard", 0 },
                    { "IVM", null, null, null, "Inventory Management", "inventory-management", 10 },
                    { "DIC", null, null, null, "Data Dictionary", "data-dictionary", 20 },
                    { "UM", null, null, null, "User Management", "user-management", 30 }
                });

            migrationBuilder.InsertData(
                table: "program_menu",
                columns: new[] { "pm_id", "pm_action_route", "pm_controller_route", "pm_icon_name", "pm_icon_provider", "pm_icon_type", "pm_name", "pm_folder", "pm_seqno" },
                values: new object[,]
                {
                    { "UM_USERGRP", "index", "user-group", null, null, null, "User Group", "UM", 20 },
                    { "UM_USERACC", "index", "user-account", null, null, null, "User Account", "UM", 10 },
                    { "DIC_ITEMGRP", "index", "item-group", null, null, null, "Item Group", "DIC", 50 },
                    { "DIC_ITEM", "index", "item-master", null, null, null, "Item Master", "DIC", 40 },
                    { "UM_USERPROF", "index", "user-profile", null, null, null, "User Profile", "UM", 30 },
                    { "DIC_SUP", "index", "supplier", null, null, null, "Supplier", "DIC", 20 },
                    { "DIC_UNIT", "index", "unit", null, null, null, "Unit", "DIC", 10 },
                    { "IVM_OUT", "index", "inventory-out", null, null, null, "Inventory Out", "IVM", 20 },
                    { "IVM_IN", "index", "inventory-in", null, null, null, "Inventory In", "IVM", 10 },
                    { "DIC_MANU", "index", "manufacturer", null, null, null, "Manufacturer", "DIC", 30 }
                });

            migrationBuilder.InsertData(
                table: "unit_code",
                columns: new[] { "uc_code", "uc_description", "uc_short_description" },
                values: new object[,]
                {
                    { "100", "Meter", null },
                    { "101", "Kilo", null },
                    { "102", "Gram", null }
                });

            migrationBuilder.InsertData(
                table: "user_account",
                columns: new[] { "ua_user_id", "im_created_by", "im_created_on", "ua_is_active", "ua_password", "im_updated_by", "im_updated_on", "ua_user_name" },
                values: new object[] { "SYSAD", "SYSTEM", new DateTime(2020, 3, 31, 16, 5, 24, 46, DateTimeKind.Local), (byte)1, ".00000", "SYSTEM", new DateTime(2020, 3, 31, 16, 5, 24, 48, DateTimeKind.Local), "SYSTEM ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "user_account_group",
                columns: new[] { "uag_user_id", "uag_group_id" },
                values: new object[] { "SYSAD", "ADMIN" });

            migrationBuilder.InsertData(
                table: "user_group",
                columns: new[] { "ug_id", "ug_name" },
                values: new object[] { "ADMIN", "SYSTEM ADMINISTRATORS" });

            migrationBuilder.InsertData(
                table: "user_group_program",
                columns: new[] { "ugp_user_group", "ugp_program_id" },
                values: new object[,]
                {
                    { "ADMIN", "IVM_IN" },
                    { "ADMIN", "IVM_OUT" },
                    { "ADMIN", "DIC_UNIT" },
                    { "ADMIN", "DIC_SUP" },
                    { "ADMIN", "DIC_MANU" },
                    { "ADMIN", "DIC_ITEM" },
                    { "ADMIN", "DIC_ITEMGRP" },
                    { "ADMIN", "UM_USERACC" },
                    { "ADMIN", "UM_USERGRP" },
                    { "ADMIN", "UM_USERPROF" }
                });

            migrationBuilder.CreateIndex(
                name: "IDX_DESCRIPTION",
                table: "unit_code",
                column: "uc_description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "event_table");

            migrationBuilder.DropTable(
                name: "item_group");

            migrationBuilder.DropTable(
                name: "item_master");

            migrationBuilder.DropTable(
                name: "item_master_unit");

            migrationBuilder.DropTable(
                name: "manufacturer");

            migrationBuilder.DropTable(
                name: "program_folder");

            migrationBuilder.DropTable(
                name: "program_menu");

            migrationBuilder.DropTable(
                name: "supplier");

            migrationBuilder.DropTable(
                name: "unit_code");

            migrationBuilder.DropTable(
                name: "user_account");

            migrationBuilder.DropTable(
                name: "user_account_group");

            migrationBuilder.DropTable(
                name: "user_group");

            migrationBuilder.DropTable(
                name: "user_group_program");
        }
    }
}
