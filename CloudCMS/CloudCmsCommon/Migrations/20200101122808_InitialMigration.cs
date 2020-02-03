using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudCmsCommon.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clinician",
                columns: table => new
                {
                    clin_id = table.Column<string>(maxLength: 20, nullable: false),
                    clin_name = table.Column<string>(maxLength: 100, nullable: false),
                    clin_email = table.Column<string>(maxLength: 100, nullable: true),
                    clin_mobile_no = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clinician", x => x.clin_id);
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
                name: "item_master",
                columns: table => new
                {
                    im_code = table.Column<string>(maxLength: 10, nullable: false),
                    im_name = table.Column<string>(maxLength: 100, nullable: false),
                    im_host_code = table.Column<string>(maxLength: 20, nullable: false),
                    im_category = table.Column<string>(maxLength: 1, nullable: false),
                    im_type = table.Column<string>(maxLength: 3, nullable: false),
                    im_cost_center = table.Column<string>(maxLength: 10, nullable: false),
                    im_remarks = table.Column<string>(maxLength: 500, nullable: true),
                    im_editable_price = table.Column<byte>(type: "TINYINT", nullable: false),
                    im_active = table.Column<byte>(type: "TINYINT", nullable: false),
                    im_created_on = table.Column<DateTime>(nullable: false),
                    im_created_by = table.Column<string>(maxLength: 20, nullable: false),
                    im_updated_on = table.Column<DateTime>(nullable: false),
                    im_updated_by = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_master", x => x.im_code);
                });

            migrationBuilder.CreateTable(
                name: "user_account",
                columns: table => new
                {
                    ua_user_id = table.Column<string>(maxLength: 20, nullable: false),
                    ua_user_name = table.Column<string>(maxLength: 100, nullable: false),
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

            migrationBuilder.InsertData(
                table: "clinician",
                columns: new[] { "clin_id", "clin_email", "clin_mobile_no", "clin_name" },
                values: new object[,]
                {
                    { "FUR", "reguis.florante@gmail.com", "09171447107", "FLORANTE U. REGUIS, MD" },
                    { "FGC", "cuizonfg@gmail.com", "09171447170", "FROILAN G. CUIZON, MD" }
                });

            migrationBuilder.InsertData(
                table: "company",
                columns: new[] { "id", "company_id", "company_name" },
                values: new object[] { "000", "NEW", "NEW CLOUD CMS CUSTOMER" });

            migrationBuilder.InsertData(
                table: "event_table",
                columns: new[] { "et_code", "et_description", "et_remarks" },
                values: new object[,]
                {
                    { "TRX_ADD", "Add transaction", null },
                    { "TRX_UPDATE", "Update transaction", null },
                    { "TRX_VOID", "Void transaction", null },
                    { "TRX_POST", "Post transaction", null },
                    { "TRX_PAY", "Pay transaction", null },
                    { "TRX_UNDO_PAY", "Undo pay transaction", null },
                    { "TRX_DELETE", "Delete transaction", null }
                });

            migrationBuilder.InsertData(
                table: "item_master",
                columns: new[] { "im_code", "im_category", "im_cost_center", "im_created_by", "im_created_on", "im_host_code", "im_active", "im_editable_price", "im_name", "im_remarks", "im_type", "im_updated_by", "im_updated_on" },
                values: new object[,]
                {
                    { "CBC", "S", "LAB", "SYSAD", new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local), "CBC", (byte)1, (byte)0, "COMPLETE BLOOD COUNT", "", "T", "SYSAD", new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local) },
                    { "FBS", "S", "LAB", "SYSAD", new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local), "FBS", (byte)1, (byte)0, "FASTING BLOOD SUGAR", "", "T", "SYSAD", new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local) },
                    { "PRE-EMP-1", "P", "0", "SYSAD", new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local), "PRE-EMP1", (byte)1, (byte)0, "PRE-EMPLOYMENT PACKAGE #1", "", "0", "SYSAD", new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local) },
                    { "URINA", "S", "LAB", "SYSAD", new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local), "URINA", (byte)1, (byte)0, "URINALYSIS", "", "T", "SYSAD", new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local) }
                });

            migrationBuilder.InsertData(
                table: "user_account",
                columns: new[] { "ua_user_id", "im_created_by", "im_created_on", "ua_is_active", "ua_password", "im_updated_by", "im_updated_on", "ua_user_name" },
                values: new object[,]
                {
                    { "SYSAD", "SYSTEM", new DateTime(2020, 1, 1, 20, 28, 7, 935, DateTimeKind.Local), (byte)1, ".00000", "SYSTEM", new DateTime(2020, 1, 1, 20, 28, 7, 936, DateTimeKind.Local), "SYSTEM ADMINISTRATOR" },
                    { "FUR", "SYSTEM", new DateTime(2020, 1, 1, 20, 28, 7, 937, DateTimeKind.Local), (byte)1, ".00000", "SYSTEM", new DateTime(2020, 1, 1, 20, 28, 7, 937, DateTimeKind.Local), "FLORANTE U. REGUIS" }
                });

            migrationBuilder.CreateIndex(
                name: "IDX_NAME",
                table: "clinician",
                column: "clin_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IDX_CATEGORY",
                table: "item_master",
                column: "im_category");

            migrationBuilder.CreateIndex(
                name: "IDX_NAME",
                table: "item_master",
                column: "im_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clinician");

            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "event_table");

            migrationBuilder.DropTable(
                name: "item_master");

            migrationBuilder.DropTable(
                name: "user_account");
        }
    }
}
