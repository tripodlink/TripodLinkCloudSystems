using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudImsCommon.Migrations
{
    public partial class AddSupplierTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 43, 44, 957, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 43, 44, 957, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 43, 44, 958, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 43, 44, 958, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 43, 44, 958, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 43, 44, 958, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 43, 44, 958, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 43, 44, 958, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FGC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 43, 44, 941, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 43, 44, 941, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 43, 44, 941, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 43, 44, 941, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "MLS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 43, 44, 941, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 43, 44, 941, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 43, 44, 939, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 43, 44, 941, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "supplier");

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 15, 4, 385, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 15, 4, 385, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 15, 4, 386, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 15, 4, 386, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 15, 4, 386, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 15, 4, 386, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 15, 4, 386, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 15, 4, 386, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FGC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 15, 4, 367, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 15, 4, 367, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 15, 4, 367, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 15, 4, 367, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "MLS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 15, 4, 367, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 15, 4, 367, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 15, 4, 365, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 15, 4, 367, DateTimeKind.Local) });
        }
    }
}
