using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudImsCommon.Migrations
{
    public partial class DefectedItemsModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "defected_items",
                columns: table => new
                {
                    defect_trxno = table.Column<string>(maxLength: 100, nullable: false),
                    item_id = table.Column<string>(maxLength: 100, nullable: false),
                    item_unit = table.Column<string>(maxLength: 100, nullable: false),
                    quantity = table.Column<double>(nullable: false),
                    lot_number = table.Column<string>(maxLength: 100, nullable: false),
                    trxno = table.Column<string>(maxLength: 100, nullable: false),
                    trx_date = table.Column<DateTime>(nullable: false),
                    remarks = table.Column<string>(maxLength: 100, nullable: false),
                    status = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_defected_items", x => x.defect_trxno);
                });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 9, 13, 0, 46, 57, 954, DateTimeKind.Local), new DateTime(2020, 9, 13, 0, 46, 57, 955, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "defected_items");

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 9, 6, 16, 25, 9, 384, DateTimeKind.Local), new DateTime(2020, 9, 6, 16, 25, 9, 385, DateTimeKind.Local) });
        }
    }
}
