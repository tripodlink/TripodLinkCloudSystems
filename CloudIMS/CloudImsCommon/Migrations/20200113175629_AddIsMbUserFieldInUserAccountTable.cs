using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudImsCommon.Migrations
{
    public partial class AddIsMbUserFieldInUserAccountTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "ua_is_mb_user",
                table: "user_account",
                type: "TINYINT",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 14, 1, 56, 28, 720, DateTimeKind.Local), new DateTime(2020, 1, 14, 1, 56, 28, 720, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 14, 1, 56, 28, 720, DateTimeKind.Local), new DateTime(2020, 1, 14, 1, 56, 28, 720, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 14, 1, 56, 28, 720, DateTimeKind.Local), new DateTime(2020, 1, 14, 1, 56, 28, 720, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 14, 1, 56, 28, 720, DateTimeKind.Local), new DateTime(2020, 1, 14, 1, 56, 28, 720, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "ua_is_mb_user", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 14, 1, 56, 28, 696, DateTimeKind.Local), (byte)1, new DateTime(2020, 1, 14, 1, 56, 28, 696, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "ua_is_mb_user", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 14, 1, 56, 28, 694, DateTimeKind.Local), (byte)1, new DateTime(2020, 1, 14, 1, 56, 28, 696, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ua_is_mb_user",
                table: "user_account");

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local), new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local), new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local), new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local), new DateTime(2020, 1, 1, 20, 28, 7, 953, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 1, 20, 28, 7, 937, DateTimeKind.Local), new DateTime(2020, 1, 1, 20, 28, 7, 937, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 1, 20, 28, 7, 935, DateTimeKind.Local), new DateTime(2020, 1, 1, 20, 28, 7, 936, DateTimeKind.Local) });
        }
    }
}
