using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudImsCommon.Migrations
{
    public partial class CreateUnitCodeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "unit_code",
                columns: table => new
                {
                    uc_code = table.Column<string>(maxLength: 3, nullable: false),
                    uc_description = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unit_code", x => x.uc_code);
                });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 26, 40, 87, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 26, 40, 87, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 26, 40, 87, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 26, 40, 87, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 26, 40, 87, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 26, 40, 87, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 26, 40, 87, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 26, 40, 87, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FGC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 26, 40, 67, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 26, 40, 67, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 26, 40, 67, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 26, 40, 67, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "MLS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 26, 40, 67, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 26, 40, 67, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 26, 40, 66, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 26, 40, 67, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "unit_code");

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 40, 24, 826, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 40, 24, 826, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 40, 24, 826, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 40, 24, 826, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 40, 24, 826, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 40, 24, 826, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 40, 24, 826, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 40, 24, 826, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FGC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 40, 24, 812, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 40, 24, 812, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 40, 24, 812, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 40, 24, 812, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "MLS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 40, 24, 812, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 40, 24, 812, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 40, 24, 810, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 40, 24, 812, DateTimeKind.Local) });
        }
    }
}
