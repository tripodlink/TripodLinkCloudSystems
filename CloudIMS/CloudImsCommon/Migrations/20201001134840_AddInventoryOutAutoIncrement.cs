using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudImsCommon.Migrations
{
    public partial class AddInventoryOutAutoIncrement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "itoh_ID",
                table: "inventoryout_trx_detail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_inventoryout_trx_detail_itoh_ID",
                table: "inventoryout_trx_detail",
                columns: new[] { "itoh_ID"});

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 10, 1, 21, 48, 40, 454, DateTimeKind.Local), new DateTime(2020, 10, 1, 21, 48, 40, 455, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "itoh_ID",
                table: "inventoryout_trx_detail");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_inventoryout_trx_detail_itoh_in_trxno_itoh_item_id_itoh_trxno",
                table: "inventoryout_trx_detail",
                columns: new[] { "itoh_in_trxno", "itoh_item_id", "itoh_trxno" });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 9, 17, 17, 58, 12, 696, DateTimeKind.Local), new DateTime(2020, 9, 17, 17, 58, 12, 697, DateTimeKind.Local) });
        }
    }
}
