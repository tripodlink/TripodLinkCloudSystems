using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudImsCommon.Migrations
{
    public partial class Add_InventoryIn_trx_header_and_detail_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 4, 13, 11, 7, 4, 20, DateTimeKind.Local), new DateTime(2020, 4, 13, 11, 7, 4, 21, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 4, 13, 10, 53, 26, 342, DateTimeKind.Local), new DateTime(2020, 4, 13, 10, 53, 26, 342, DateTimeKind.Local) });
        }
    }
}
