using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudImsCommon.Migrations
{
    public partial class UpdateItemMasterAddMinimumStockLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "im_unit",
                table: "item_master",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddColumn<double>(
                name: "im_min_stock_lvl",
                table: "item_master",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "itoh_issued_by",
                table: "inventoryout_trx_header",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.InsertData(
                table: "Auto_Number",
                columns: new[] { "an_type", "an_auto_length", "an_current_year", "an_date_prefix", "an_last_value", "an_text_prefix" },
                values: new object[] { "INVIT", "8", "2020", "YY", "1", "IT" });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 9, 17, 17, 58, 12, 696, DateTimeKind.Local), new DateTime(2020, 9, 17, 17, 58, 12, 697, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Auto_Number",
                keyColumn: "an_type",
                keyValue: "INVIT");

            migrationBuilder.DropColumn(
                name: "im_min_stock_lvl",
                table: "item_master");

            migrationBuilder.AlterColumn<string>(
                name: "im_unit",
                table: "item_master",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "itoh_issued_by",
                table: "inventoryout_trx_header",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 9, 6, 23, 34, 59, 844, DateTimeKind.Local), new DateTime(2020, 9, 6, 23, 34, 59, 846, DateTimeKind.Local) });
        }
    }
}
