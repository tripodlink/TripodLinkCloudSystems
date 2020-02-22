﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudImsCommon.Migrations
{
    public partial class InitialMigration : Migration
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
                name: "program_folder",
                columns: table => new
                {
                    pf_folder_id = table.Column<string>(maxLength: 10, nullable: false),
                    pf_folder_name = table.Column<string>(maxLength: 100, nullable: false),
                    pf_folder_route = table.Column<string>(maxLength: 25, nullable: false),
                    pf_icon_type = table.Column<string>(maxLength: 10, nullable: true),
                    pf_icon_provider = table.Column<string>(maxLength: 20, nullable: true),
                    pf_icon = table.Column<string>(maxLength: 10, nullable: true),
                    pf_seqno = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_program_folder", x => x.pf_folder_id);
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
                    ua_user_id = table.Column<string>(maxLength: 20, nullable: false),
                    ua_user_name = table.Column<string>(maxLength: 100, nullable: false),
                    ua_password = table.Column<string>(maxLength: 100, nullable: false),
                    ua_is_active = table.Column<byte>(type: "TINYINT", nullable: false),
                    ua_is_mb_user = table.Column<byte>(type: "TINYINT", nullable: false),
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
                name: "program_menu",
                columns: table => new
                {
                    pm_id = table.Column<string>(maxLength: 20, nullable: false),
                    pm_name = table.Column<string>(maxLength: 100, nullable: false),
                    pm_folder = table.Column<string>(maxLength: 10, nullable: false),
                    pm_folder_route = table.Column<string>(maxLength: 25, nullable: false),
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
                    table.ForeignKey(
                        name: "FK_program_menu_program_folder_pm_folder",
                        column: x => x.pm_folder,
                        principalTable: "program_folder",
                        principalColumn: "pf_folder_id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_user_account_group_user_account_uag_user_id",
                        column: x => x.uag_user_id,
                        principalTable: "user_account",
                        principalColumn: "ua_user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_account_group_user_group_uag_group_id",
                        column: x => x.uag_group_id,
                        principalTable: "user_group",
                        principalColumn: "ug_id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_user_group_program_program_menu_ugp_program_id",
                        column: x => x.ugp_program_id,
                        principalTable: "program_menu",
                        principalColumn: "pm_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_group_program_user_group_ugp_user_group",
                        column: x => x.ugp_user_group,
                        principalTable: "user_group",
                        principalColumn: "ug_id",
                        onDelete: ReferentialAction.Cascade);
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
                columns: new[] { "ua_user_id", "im_created_by", "im_created_on", "ua_is_active", "ua_is_mb_user", "ua_password", "im_updated_by", "im_updated_on", "ua_user_name" },
                values: new object[] { "SYSAD", "SYSTEM", new DateTime(2020, 2, 22, 19, 38, 27, 569, DateTimeKind.Local), (byte)1, (byte)1, ".00000", "SYSTEM", new DateTime(2020, 2, 22, 19, 38, 27, 570, DateTimeKind.Local), "SYSTEM ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "user_group",
                columns: new[] { "ug_id", "ug_name" },
                values: new object[] { "ADMIN", "SYSTEM ADMINISTRATORS" });

            migrationBuilder.InsertData(
                table: "program_menu",
                columns: new[] { "pm_id", "pm_action_route", "pm_controller_route", "pm_folder_route", "pm_icon_name", "pm_icon_provider", "pm_icon_type", "pm_name", "pm_folder", "pm_seqno" },
                values: new object[,]
                {
                    { "IVM_IN", "index", "inventory-in", "inventory-management", null, null, null, "Inventory In", "IVM", 10 },
                    { "IVM_OUT", "index", "inventory-out", "inventory-management", null, null, null, "Inventory Out", "IVM", 20 },
                    { "DIC_UNIT", "index", "unit", "data-dictionary", null, null, null, "Unit", "DIC", 10 },
                    { "DIC_SUP", "index", "supplier", "data-dictionary", null, null, null, "Supplier", "DIC", 20 },
                    { "DIC_MANU", "index", "manufacturer", "data-dictionary", null, null, null, "Manufacturer", "DIC", 30 },
                    { "DIC_ITEM", "index", "item-master", "data-dictionary", null, null, null, "Item Master", "DIC", 40 },
                    { "DIC_ITEMGRP", "index", "item-group", "data-dictionary", null, null, null, "Item Group", "DIC", 50 },
                    { "UM_USERACC", "index", "user-account", "user-management", null, null, null, "User Account", "UM", 10 },
                    { "UM_USERGRP", "index", "user-group", "user-management", null, null, null, "User Group", "UM", 20 },
                    { "UM_USERPROF", "index", "user-profile", "user-management", null, null, null, "User Profile", "UM", 30 }
                });

            migrationBuilder.InsertData(
                table: "user_account_group",
                columns: new[] { "uag_user_id", "uag_group_id" },
                values: new object[] { "SYSAD", "ADMIN" });

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
                name: "IX_program_menu_pm_folder",
                table: "program_menu",
                column: "pm_folder");

            migrationBuilder.CreateIndex(
                name: "IDX_DESCRIPTION",
                table: "unit_code",
                column: "uc_description");

            migrationBuilder.CreateIndex(
                name: "IX_user_account_group_uag_group_id",
                table: "user_account_group",
                column: "uag_group_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "event_table");

            migrationBuilder.DropTable(
                name: "supplier");

            migrationBuilder.DropTable(
                name: "unit_code");

            migrationBuilder.DropTable(
                name: "user_account_group");

            migrationBuilder.DropTable(
                name: "user_group_program");

            migrationBuilder.DropTable(
                name: "user_account");

            migrationBuilder.DropTable(
                name: "program_menu");

            migrationBuilder.DropTable(
                name: "user_group");

            migrationBuilder.DropTable(
                name: "program_folder");
        }
    }
}
