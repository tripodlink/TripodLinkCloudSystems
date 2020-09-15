using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudImsCommon.Migrations
{
    public partial class ItemTracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "item_tracking",
                columns: table => new
                {
                    it_trxno = table.Column<string>(maxLength: 25, nullable: false),
                    it_item_id = table.Column<string>(maxLength: 250, nullable: false),
                    it_lot_no = table.Column<string>(maxLength: 250, nullable: false),
                    it_date_updated = table.Column<string>(maxLength: 250, nullable: false),
                    it_location = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_tracking", x => x.it_trxno);
                });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 9, 6, 15, 9, 30, 312, DateTimeKind.Local), new DateTime(2020, 9, 6, 15, 9, 30, 314, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "item_tracking");

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 9, 6, 14, 59, 40, 968, DateTimeKind.Local), new DateTime(2020, 9, 6, 14, 59, 40, 971, DateTimeKind.Local) });
        }
    }
}
