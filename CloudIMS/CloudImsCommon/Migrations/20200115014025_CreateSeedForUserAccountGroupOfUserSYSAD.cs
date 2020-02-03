using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudCmsCommon.Migrations
{
    public partial class CreateSeedForUserAccountGroupOfUserSYSAD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "user_account_group",
                columns: new[] { "UserAccountID", "UserGroupID" },
                values: new object[,]
                {
                    { "SYSAD", "GENADMIN" },
                    { "SYSAD", "BBADMIN" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "user_account_group",
                keyColumns: new[] { "UserAccountID", "UserGroupID" },
                keyValues: new object[] { "SYSAD", "BBADMIN" });

            migrationBuilder.DeleteData(
                table: "user_account_group",
                keyColumns: new[] { "UserAccountID", "UserGroupID" },
                keyValues: new object[] { "SYSAD", "GENADMIN" });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 33, 38, 335, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 33, 38, 335, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 33, 38, 335, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 33, 38, 335, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 33, 38, 335, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 33, 38, 335, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 33, 38, 335, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 33, 38, 335, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FGC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 33, 38, 320, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 33, 38, 320, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 33, 38, 320, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 33, 38, 320, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "MLS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 33, 38, 320, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 33, 38, 320, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 33, 38, 319, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 33, 38, 320, DateTimeKind.Local) });
        }
    }
}
