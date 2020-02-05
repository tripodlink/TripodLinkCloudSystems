using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudImsCommon.Migrations
{
    public partial class AddGramUnitCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 47, 45, 503, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 47, 45, 503, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 47, 45, 503, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 47, 45, 503, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 47, 45, 503, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 47, 45, 503, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 47, 45, 503, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 47, 45, 503, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "unit_code",
                columns: new[] { "uc_code", "uc_description", "uc_short_description" },
                values: new object[] { "102", "Gram", null });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FGC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 47, 45, 481, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 47, 45, 481, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 47, 45, 481, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 47, 45, 481, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "MLS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 47, 45, 481, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 47, 45, 481, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 47, 45, 479, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 47, 45, 481, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "unit_code",
                keyColumn: "uc_code",
                keyValue: "102");

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
    }
}
