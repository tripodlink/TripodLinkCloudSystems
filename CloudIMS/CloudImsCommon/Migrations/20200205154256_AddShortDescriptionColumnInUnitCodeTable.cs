using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudImsCommon.Migrations
{
    public partial class AddShortDescriptionColumnInUnitCodeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "uc_short_description",
                table: "unit_code",
                maxLength: 10,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 42, 55, 942, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 42, 55, 942, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 42, 55, 942, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 42, 55, 942, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 42, 55, 942, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 42, 55, 942, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 42, 55, 942, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 42, 55, 942, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FGC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 42, 55, 927, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 42, 55, 927, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 42, 55, 927, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 42, 55, 927, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "MLS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 42, 55, 927, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 42, 55, 927, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 42, 55, 925, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 42, 55, 927, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "uc_short_description",
                table: "unit_code");

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 38, 12, 37, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 38, 12, 37, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 38, 12, 37, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 38, 12, 37, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 38, 12, 37, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 38, 12, 37, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 38, 12, 37, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 38, 12, 37, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FGC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 38, 12, 17, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 38, 12, 17, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 38, 12, 17, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 38, 12, 17, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "MLS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 38, 12, 17, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 38, 12, 17, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 38, 12, 15, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 38, 12, 17, DateTimeKind.Local) });
        }
    }
}
