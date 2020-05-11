using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudImsCommon.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auto_Number",
                columns: table => new
                {
                    an_type = table.Column<string>(maxLength: 100, nullable: false),
                    an_text_prefix = table.Column<string>(maxLength: 255, nullable: false),
                    an_date_prefix = table.Column<string>(maxLength: 255, nullable: false),
                    an_auto_length = table.Column<string>(maxLength: 255, nullable: false),
                    an_last_value = table.Column<string>(maxLength: 255, nullable: false),
                    an_current_year = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auto_Number", x => x.an_type);
                });

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
                name: "department",
                columns: table => new
                {
                    d_id = table.Column<string>(maxLength: 15, nullable: false),
                    d_depertment_name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.d_id);
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
                name: "inventoryin_trx_detail",
                columns: table => new
                {
                    trxno = table.Column<string>(maxLength: 100, nullable: false),
                    item_id = table.Column<string>(maxLength: 100, nullable: false),
                    unit = table.Column<string>(maxLength: 100, nullable: false),
                    quantity = table.Column<int>(nullable: false),
                    lotno = table.Column<string>(maxLength: 100, nullable: false),
                    exp_date = table.Column<DateTime>(nullable: false),
                    count = table.Column<double>(nullable: false),
                    remaning_count = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventoryin_trx_detail", x => x.trxno);
                });

            migrationBuilder.CreateTable(
                name: "inventoryin_trx_header",
                columns: table => new
                {
                    trxno = table.Column<string>(maxLength: 100, nullable: false),
                    trx_date = table.Column<DateTime>(nullable: false),
                    rcvd_date = table.Column<DateTime>(nullable: false),
                    rcvd_by = table.Column<string>(maxLength: 100, nullable: true),
                    po_number = table.Column<string>(maxLength: 100, nullable: true),
                    invoice_number = table.Column<string>(maxLength: 100, nullable: true),
                    ref_number = table.Column<string>(maxLength: 100, nullable: true),
                    doc_number = table.Column<string>(maxLength: 100, nullable: true),
                    supplier = table.Column<string>(maxLength: 100, nullable: true),
                    remarks = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventoryin_trx_header", x => x.trxno);
                });

            migrationBuilder.CreateTable(
                name: "inventoryout_trx_detail",
                columns: table => new
                {
                    itoh_trxno = table.Column<string>(maxLength: 100, nullable: false),
                    itoh_item_id = table.Column<string>(maxLength: 100, nullable: false),
                    itoh_unit = table.Column<string>(maxLength: 100, nullable: false),
                    itoh_in_trxno = table.Column<string>(maxLength: 100, nullable: false),
                    itoh_quantity = table.Column<double>(nullable: false),
                    itoh_remarks = table.Column<string>(maxLength: 300, nullable: true),
                    itoh_mincount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventoryout_trx_detail", x => new { x.itoh_trxno, x.itoh_item_id, x.itoh_in_trxno });
                    table.UniqueConstraint("AK_inventoryout_trx_detail_itoh_in_trxno_itoh_item_id_itoh_trxno", x => new { x.itoh_in_trxno, x.itoh_item_id, x.itoh_trxno });
                });

            migrationBuilder.CreateTable(
                name: "inventoryout_trx_header",
                columns: table => new
                {
                    itoh_trxno = table.Column<string>(maxLength: 100, nullable: false),
                    itoh_trx_date = table.Column<DateTime>(nullable: false),
                    itoh_issued_by = table.Column<string>(maxLength: 100, nullable: false),
                    itoh_issued_date = table.Column<DateTime>(nullable: false),
                    itoh_rcvd_by = table.Column<string>(maxLength: 100, nullable: false),
                    itoh_department = table.Column<string>(maxLength: 200, nullable: false),
                    itoh_ref_number = table.Column<string>(maxLength: 100, nullable: false),
                    itoh_remarks = table.Column<string>(maxLength: 100, nullable: true),
                    itoh_status = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventoryout_trx_header", x => x.itoh_trxno);
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
                    pf_folder_id = table.Column<string>(maxLength: 10, nullable: false),
                    pf_folder_name = table.Column<string>(maxLength: 100, nullable: false),
                    pf_folder_route = table.Column<string>(maxLength: 25, nullable: false),
                    pf_icon_type = table.Column<string>(maxLength: 10, nullable: true),
                    pf_icon_provider = table.Column<string>(maxLength: 20, nullable: true),
                    pf_icon = table.Column<string>(maxLength: 100, nullable: true),
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
                    pm_route = table.Column<string>(maxLength: 25, nullable: false),
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
                table: "department",
                columns: new[] { "d_id", "d_depertment_name" },
                values: new object[] { "100", "Default DepartmentName" });

            migrationBuilder.InsertData(
                table: "event_table",
                columns: new[] { "et_code", "et_description", "et_remarks" },
                values: new object[] { "IV_IN", "Inventory In", null });

            migrationBuilder.InsertData(
                table: "item_group",
                columns: new[] { "ig_id", "ig_item_group_name" },
                values: new object[] { "100", "Default ItemGroup" });

            migrationBuilder.InsertData(
                table: "item_master",
                columns: new[] { "im_id", "im_item_group", "im_item_name", "im_manufact", "im_supp", "im_unit" },
                values: new object[] { "01", "100", "Default Item", "100", "100", "102" });

            migrationBuilder.InsertData(
                table: "item_master_unit",
                columns: new[] { "imu_id", "imu_unit", "im_item_conversion" },
                values: new object[] { "01", "102", "1" });

            migrationBuilder.InsertData(
                table: "manufacturer",
                columns: new[] { "m_id", "m_item_group_name" },
                values: new object[] { "100", "Default Manufacturer" });

            migrationBuilder.InsertData(
                table: "program_folder",
                columns: new[] { "pf_folder_id", "pf_icon", "pf_icon_provider", "pf_icon_type", "pf_folder_name", "pf_folder_route", "pf_seqno" },
                values: new object[,]
                {
                    { "RPT", "fa fa-file-text", null, null, "Report Management", "report-management", 10 },
                    { "DASH", "fa fa-tachometer", null, null, "Dashboard", "dashboard", 10 },
                    { "DIC", "fa fa-file-code-o", null, null, "Data Dictionary", "data-dictionary", 20 },
                    { "IVM", "fa fa-truck", null, null, "Inventory Management", "inventory-management", 10 },
                    { "UM", "fa fa-user-circle-o", null, null, "User Management", "user-management", 30 }
                });

            migrationBuilder.InsertData(
                table: "program_menu",
                columns: new[] { "pm_id", "pm_icon_name", "pm_icon_provider", "pm_icon_type", "pm_name", "pm_folder", "pm_route", "pm_seqno" },
                values: new object[,]
                {
                    { "UM_USERACC", "fa fa-desktop", null, null, "User Account", "UM", "user-account", 10 },
                    { "RPT_IVM_OUT", "fa fa-desktop", null, null, "(Report) Inventory Out", "RPT", "report-inventory-out", 20 },
                    { "RPT_IVM_IN", "fa fa-desktop", null, null, "(Report) Inventory In", "RPT", "report-inventory-in", 10 },
                    { "UM_USERPROF", "fa fa-desktop", null, null, "User Profile", "UM", "user-profile", 30 },
                    { "UM_USERGRP", "fa fa-desktop", null, null, "User Group", "UM", "user-group", 20 },
                    { "DIC_DEP", "fa fa-desktop", null, null, "Department", "DIC", "department", 60 },
                    { "DASH", "fa fa-tachometer", null, null, "Dashboard", "DASH", "dashboard", 10 },
                    { "DIC_ITEM", "fa fa-desktop", null, null, "Item Master", "DIC", "item-master", 40 },
                    { "DIC_MANU", "fa fa-desktop", null, null, "Manufacturer", "DIC", "manufacturer", 30 },
                    { "DIC_SUP", "fa fa-desktop", null, null, "Supplier", "DIC", "supplier", 20 },
                    { "DIC_UNIT", "fa fa-desktop", null, null, "Unit", "DIC", "unit-code", 10 },
                    { "DIC_ITEMGRP", "fa fa-desktop", null, null, "Item Group", "DIC", "item-group", 50 },
                    { "IVM_OUT", "fa fa-desktop", null, null, "Inventory Out", "IVM", "inventory-out", 20 },
                    { "IVM_IN", "fa fa-desktop", null, null, "Inventory In", "IVM", "inventory-in", 10 }
                });

            migrationBuilder.InsertData(
                table: "supplier",
                columns: new[] { "sup_id", "sup_name" },
                values: new object[] { "100", "Default Supplier" });

            migrationBuilder.InsertData(
                table: "unit_code",
                columns: new[] { "uc_code", "uc_description", "uc_short_description" },
                values: new object[,]
                {
                    { "100", "Box", null },
                    { "101", "Tray", null },
                    { "102", "Pieces", null }
                });

            migrationBuilder.InsertData(
                table: "user_account",
                columns: new[] { "ua_user_id", "im_created_by", "im_created_on", "ua_is_active", "ua_password", "im_updated_by", "im_updated_on", "ua_user_name" },
                values: new object[] { "SYSAD", "SYSTEM", new DateTime(2020, 5, 6, 17, 51, 57, 928, DateTimeKind.Local), (byte)1, ".00000", "SYSTEM", new DateTime(2020, 5, 6, 17, 51, 57, 930, DateTimeKind.Local), "SYSTEM ADMINISTRATOR" });

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
                    { "ADMIN", "RPT_IVM_IN" },
                    { "ADMIN", "RPT_IVM_OUT" },
                    { "ADMIN", "IVM_IN" },
                    { "ADMIN", "IVM_OUT" },
                    { "ADMIN", "DIC_UNIT" },
                    { "ADMIN", "DIC_SUP" },
                    { "ADMIN", "DIC_MANU" },
                    { "ADMIN", "DIC_ITEM" },
                    { "ADMIN", "DIC_ITEMGRP" },
                    { "ADMIN", "DIC_DEP" },
                    { "ADMIN", "DASH" },
                    { "ADMIN", "UM_USERACC" },
                    { "ADMIN", "UM_USERGRP" },
                    { "ADMIN", "UM_USERPROF" }
                });

            migrationBuilder.CreateIndex(
                name: "IDX_DESCRIPTION",
                table: "department",
                column: "d_depertment_name");

            migrationBuilder.CreateIndex(
                name: "IDX_DESCRIPTION",
                table: "item_group",
                column: "ig_item_group_name");

            migrationBuilder.CreateIndex(
                name: "IDX_DESCRIPTION",
                table: "manufacturer",
                column: "m_item_group_name");

            migrationBuilder.CreateIndex(
                name: "IDX_DESCRIPTION",
                table: "supplier",
                column: "sup_name");

            migrationBuilder.CreateIndex(
                name: "IDX_DESCRIPTION",
                table: "unit_code",
                column: "uc_description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auto_Number");

            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "department");

            migrationBuilder.DropTable(
                name: "event_table");

            migrationBuilder.DropTable(
                name: "inventoryin_trx_detail");

            migrationBuilder.DropTable(
                name: "inventoryin_trx_header");

            migrationBuilder.DropTable(
                name: "inventoryout_trx_detail");

            migrationBuilder.DropTable(
                name: "inventoryout_trx_header");

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
